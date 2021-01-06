using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * BaseAgent: clase base de los comportamientos de los agentes. Todos los agentes usarán esta
 * clase o una heredada de esta.
 */
public class BaseAgent : MonoBehaviour
{
    public List<RaycastHit> hits;
    public float maxPerceptionL;
    public int maxPerceptionA;

    public string predatorTag;
    public string[] foodTag = new string[3];

    public NavMeshAgent _agent;

    protected NavMeshAgent _evadeTarget, _pursuitTarget;
    private Transform _pursuitPlant;

    protected float _wanderRadius = 5;

    public bool isHungry;
    public float hunger;

    public bool canReproduce;
    [SerializeField]  GameObject hijoPrefab;
    private Time repTime;
    private float waitingTime;

    public List<float> dna;
    public List<float> perfectDna;
    public List<float> worstDna;
    protected float mutationRate;

    private bool initialized = false;

    public bool noHunger = false;
    private float _baseSpeed;

    protected enum state
    {
        None,
        Wander,
        Pursuit,
        Evade,
        FrogOut,
        FrogIn
    }

    [HideInInspector] public enum tagsAgents
    {
        //dinamicos
        Pez,
        Mosca,
        Renacuajo,
        Rana,
        //estaticos
        HuevosRana,
        HuevosPez,
        Plantas

    }
    [SerializeField] protected state currentState;

    #region Unity Functions
    /*
     * Awake: define los tag de depredador y de comida de cada agente nada
     * más instanciarlo.
     */
    private void Awake()
    {
        SetTags();
    }

    /*
     * Start: inicializa atributos.
     */
    protected virtual void Start()
    {
        hits = new List<RaycastHit>();
        maxPerceptionA = 15;
        maxPerceptionL = 10;
        hunger = 1;
        waitingTime = 0;
        _agent = GetComponent<NavMeshAgent>();
        if (dna.Count > 0)
        {
            _agent.speed = dna[0];
            _baseSpeed = dna[0];
            _agent.acceleration = dna[1];
        }
    }

    /*
     * FixedUpdate: llama al método encargado de detectar al resto de los agentes, tramita 
     * el hambre y la reproducción de los agentes.
     */
    protected virtual void FixedUpdate()
    {
        Vision();
        if (!noHunger) hunger -= ((0.25f-dna[2])*0.5f)*Time.fixedDeltaTime;
        waitingTime += Time.deltaTime;
        if (hunger < 0.6 && !isHungry) isHungry = true;

        if (hunger < 0) DeadAction();
        if (/*currentState.Equals(state.Wander) && */((gameObject.tag.Equals("Pez") || gameObject.tag.Equals("Rana")) && waitingTime > dna[4]*2))
        {
            if (gameObject.tag.Equals("Pez") && gameObject.GetComponent<Fish>().juvenile)
            {
                canReproduce = false;
            }
            else
            {
                //Debug.DrawRay(transform.position, transform.forward);
                canReproduce = hunger > 0.2;
            }
        }
        else { canReproduce = false; }

        if (isHungry || canReproduce) 
        {
            _agent.speed = _baseSpeed*1.5f;
        }
        else
        {
            _agent.speed = _baseSpeed;
        }
    }

    /*
     * OnTriggerEnter: método por el cual el agente se come a un agente si está 
     * hambriento y si es uno de los agentes que puede comer.
     */
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != null && isHungry)
        {
            for (int i = 0; i < foodTag.Length; i++)
            {
                if (!string.IsNullOrEmpty(foodTag[i]))
                {
                    if (collision.gameObject.CompareTag(foodTag[i]))
                    {
                        if (collision.gameObject.CompareTag("Pez") && collision.gameObject.GetComponent<Fish>() != null && collision.gameObject.GetComponent<Fish>().juvenile == false) return;
                        if ((!collision.gameObject.CompareTag("Plantas")))
                        {//print(collision.tag);print(collision.GetComponent<LifeCycle>().dna);
                            EatAction(collision.GetComponent<LifeCycle>().dna[7]);
                        }
                        if (collision.gameObject.CompareTag("Plantas"))
                        {
                            EatAction(1);
                            if (collision.gameObject.GetComponent<LifeCycle>().canBeEaten)
                                collision.gameObject.GetComponent<LifeCycle>().EatPlant();
                        }
                        else if (collision.gameObject.CompareTag("HuevosRana") || collision.gameObject.CompareTag("HuevosPez"))
                        {
                            collision.GetComponent<Roe>().DeadRoe();
                        }
                        else
                        {
                            collision.gameObject.GetComponent<BaseAgent>().DeadAction();
                        }
                    }
                }
                else return;
            }
        }
        BaseAgent otherAgent = collision.gameObject.GetComponent<BaseAgent>();
        if (gameObject.tag == collision.tag && canReproduce && otherAgent.canReproduce)
        {
            canReproduce = false;
            otherAgent.canReproduce = false;
            Reproduction(otherAgent);
        }
    }
    #endregion Unity Functions

    #region Functions
    /*
     * Init: inicializa el ADN del agente y recibe también las listas del mejor y peor ADN (valores máximos y mínimos respectivamente).
     */
    public void Init(List<float> d, List<float> perfect, List<float> worst, float hunger)
    {

        dna = d;
        worstDna = worst;
        perfectDna = perfect;
        initialized = true;
        this.hunger = hunger;
    }

    /*
     * SetTags: guarda el tag de su depredador y un array de los tags de su comida.
     */
    private void SetTags()
    {
        switch (gameObject.tag)
        {
            case "Pez":
                predatorTag = tagsAgents.Rana.ToString();
                foodTag[0] = tagsAgents.HuevosRana.ToString();
                foodTag[1] = tagsAgents.Renacuajo.ToString();
                break;
            case "Mosca":
                predatorTag = null; //tiene depredador pero no huirá de él
                foodTag[0] = null;
                break;
            case "Renacuajo":
                predatorTag = tagsAgents.Pez.ToString();
                foodTag[0] = tagsAgents.Plantas.ToString();
                break;
            case "Rana":
                predatorTag = null;
                foodTag[0] = tagsAgents.Mosca.ToString();
                foodTag[1] = tagsAgents.Pez.ToString();
                foodTag[2] = tagsAgents.Plantas.ToString();
                break;
        }
    }

    /*
     * Vision: lanza un cono de rayos mediante el cual detecta a los agentes cercanos.
     */
    protected void Vision()
    {
        hits.Clear();
        for (int i = -maxPerceptionA; i < maxPerceptionA; i += 1)
        {
            Vector2 v = Quaternion.Euler(0, 0, i) * transform.forward;
            RaycastHit hit;
            Physics.Raycast(transform.position, v, out hit, maxPerceptionL);
            Debug.DrawRay(transform.position, v * maxPerceptionL, hit.collider!=null?Color.red:Color.green);
            hits.Add(hit);
        }

        ChangeState(state.Wander);
        foreach (RaycastHit h in hits)
        {
            if (h.collider != null)
            {
                if (CollisionDetected(h.collider)) return;
            }
        }
    }

    /*
     * Collision Detected: método llamado cuando un rayo choca con un agente. 
     * Dependiendo de su tag y diferentes parámetros, cambiará o no de estado.
     */
    protected bool CollisionDetected (Collider collider)
    {
        BaseAgent otherAgent = collider.gameObject.GetComponent<BaseAgent>();
        if (gameObject.tag == collider.tag && canReproduce && otherAgent.canReproduce)
        {
            _pursuitTarget = otherAgent._agent;
            ChangeState(state.Pursuit);
            //otherAgent._pursuitTarget = _agent;
            //otherAgent.ChangeState(state.Pursuit);
        }
        if (gameObject.tag == collider.tag)
        {
            return false;
        }
        if (!string.IsNullOrEmpty(predatorTag) && collider.gameObject.CompareTag(predatorTag))
        {
            NavMeshAgent colAgent = collider.gameObject.GetComponent<NavMeshAgent>();
            if (colAgent == null) return false;
            _evadeTarget = colAgent;
            ChangeState(state.Evade);
            //huir
            return true;
        }
        else
        {
            if (!collider.gameObject.CompareTag(gameObject.tag) && isHungry && (_pursuitTarget != null && !CompareTag(_pursuitTarget.tag)))
            {
                for (int i = 0; i < foodTag.Length; i++)
                {
                    if (!string.IsNullOrEmpty(foodTag[i]) && collider.CompareTag(foodTag[i]))
                    {
                        if (collider.CompareTag(tagsAgents.Plantas.ToString()))
                        {
                            if (collider.gameObject.GetComponent<LifeCycle>().canBeEaten)
                            {
                                _pursuitPlant = collider.transform;
                                _pursuitTarget = null;
                                ChangeState(state.Pursuit);
                            }
                        }
                        else
                        {
                            NavMeshAgent colAgent = collider.gameObject.GetComponent<NavMeshAgent>();
                            if (colAgent == null) return false;
                            _pursuitTarget = colAgent;
                            ChangeState(state.Pursuit);
                            _pursuitPlant = null;
                        }
                        //perseguir
                        return false;
                    }
                }
            }
        }
        return false;
    }

   /*
    * ChangeState: método encargado de cambiar el estado del agente por 
    * otro de los definidos en la máquina de estados.
    */
    protected virtual void ChangeState(state nextState) {

        if ((currentState == state.Evade && nextState == state.Wander && _evadeTarget != null) || 
        (currentState == state.Pursuit && nextState == state.Wander && _pursuitTarget != null) || 
        (currentState == state.Evade && nextState == state.Pursuit) || 
        currentState == nextState) return;

        StopCoroutine(currentState.ToString());
        currentState = nextState;
        StartCoroutine(currentState.ToString());
    }

    /*
     * WanderAction: Movimiento que realiza el agente cuando se encuentra en estado Wander.
     */
    protected virtual void WanderAction()
    {
        _agent.SetDestination(transform.position + transform.forward*_agent.speed + Random.insideUnitSphere*_wanderRadius);
    }

    /*
     * PursuitAction: Comportamiento que realiza el agente cuando se encuentra en estado de perseguir.
     */
    protected void PursuitAction()
    {
        if (_pursuitTarget == null || _pursuitTarget.gameObject == null || Vector3.Distance(transform.position, _pursuitTarget.transform.position) > maxPerceptionL) 
        {
            _pursuitTarget = null;
            if (_pursuitPlant != null) {_agent.SetDestination(_pursuitPlant.position);Debug.DrawRay(transform.position, _pursuitPlant.position-transform.position, Color.green);}
            return;
        }
        //Vector3 targetPos = _pursuitTarget.gameObject.transform.position + _pursuitTarget.gameObject.transform.forward * _pursuitTarget.speed;
        _agent.SetDestination(_pursuitTarget.gameObject.transform.position);Debug.DrawRay(transform.position, _pursuitTarget.gameObject.transform.position-transform.position, Color.red);
        //_agent.SetDestination(targetPos);Debug.DrawRay(transform.position, targetPos-transform.position, Color.magenta);
    }

    /*
     * EvadeAction: Acccion que realiza el agente cuando está en estado de escaapar al ver a un depredador.
     */
    protected void EvadeAction()
    {
        if (_evadeTarget == null || _evadeTarget.gameObject == null || Vector3.Distance(transform.position, _evadeTarget.transform.position) > maxPerceptionL) 
        {
            _evadeTarget = null;
            return;
        }
        Vector3 targetPos = transform.position - _evadeTarget.transform.position + _evadeTarget.transform.forward * _evadeTarget.speed;
         _agent.SetDestination(targetPos);
    }

    /*
     * EatAction: método que se encarga de saciar el hambre del agente cuando este se come a otro agente 
     * de los que puede comer:
     */
    protected virtual void EatAction(float nutrition)
    {
        hunger += nutrition;
        if(hunger > 0.6)
        {
            isHungry = false;
        }
        if(hunger > 1)
        {
            hunger = 1;
        }
    }

    /*
     * FrogOutAction: Acción que realiza la rana cuando necesita oxígeno y sale del estanque. Este
     * método es sobrescrito por el agente rana donde se definirá el comportamiento a seguir.
     */
    protected virtual void FrogOutAction() {}

    /*
     * FrogInAction: Acción que realiza la rana cuando necesita hidratarse y entra en el estanque.
     * Este método es sobrescrito por el agente rana donde se definirá el comportamiento a seguir.
     */
    protected virtual void FrogInAction() {}
    
    /*
     * DeadAction: Método encargado de tramitar la muerte del agente.
     * 
     */
    public virtual void DeadAction()
    {        
        StopAllCoroutines();
        if (gameObject.GetComponent<LifeCycle>().isTarget) gameObject.GetComponent<Clickeable>().Dead();
        Destroy(gameObject);
    }
    
    /*
     * Reproduction: método que tramita la reproducción de los agentes. Si la reproducción sale bien, llamarán al método CrossOver que se
     * encargará de instanciar a los nuevos agentes.
     */
    public void Reproduction(BaseAgent otherParent)
    {
        waitingTime = 0;
        if (otherParent.tag == "Pez")
        {
            if(gameObject.GetComponent<Fish>().juvenile || otherParent.GetComponent<Fish>().juvenile)
            {
                return;
            }
        }
        else if(otherParent.tag == "Rana")
        {
            if(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2) > Mathf.Pow(20.5f, 2))
            {
                return;
            }
        }
        
        
        float other = otherParent.CalculateFitness();
        double av = (CalculateFitness() + other) / 2;
        // Las posibilidades de reproduccion son mas altas conforme mayor sea el fitness de ambos
        if (Random.Range(0, 100) < av)
        {
            float offspringNumber = (dna[3] + otherParent.dna[3]) / 2;
            //Debug.Log(offspringNumber);
            for (int i = 0; i < offspringNumber; i++)
            {
                Crossover(otherParent);
            }
        }
    }

    /*
     * CrossOver: tras el éxito de la reproducción, se instanciarán nuevos agentes que
     * utilizarán los ADNs de los padres para crear el suyo propio.
     */
    public void Crossover(BaseAgent otherParent)
    {
        //Debug.Log("Me he reproducido");
        List<float> newDna = new List<float>();
        float stat;
        for (int i = 0; i < dna.Count; i++)
        {
            stat = Random.value < 0.5 ? dna[i] : otherParent.dna[i];
            newDna.Add(stat);
        }
        Roe roe = Instantiate(hijoPrefab, transform.position, Quaternion.identity) .GetComponent<Roe>();
        roe.Init(newDna, perfectDna, worstDna);

        roe.Mutate();
    }

    /*
     * CalculateFitness: método utilizado en la función Reproduction para saber si esta saldrá de forma exitosa o no.
     */
    protected float CalculateFitness()
    {
        float score;
        float tanto = 0;
        for (int i = 0; i < perfectDna.Count - 1; i++)
        {
            tanto += (float)((dna[i] - worstDna[i]) / (perfectDna[i] - worstDna[i])) * 100;
        }
        score = (float)tanto / (dna.Count-1);

        return score;
    }
    #endregion Functions

    #region Coroutines
    //coroutines

    /*
     * Wander: corrutina del estado Wander que llamará al método WanderAction mientras que el 
     * agente se encuentre en ese estado.
     */
    IEnumerator Wander()
    {
        while (currentState.Equals(state.Wander))
        {
            WanderAction();
            yield return 0;
        }
    }

    /*
     * Pursuit: corrutina del estado Pursuit que llamará al método PursuitAction mientras que el 
     * agente se encuentre en ese estado.
     */
    IEnumerator Pursuit()
    {
        while (currentState.Equals(state.Pursuit))
        {
            PursuitAction();
            yield return 0;
        }
    }

    /*
     * Evade: corrutina del estado Evade que llamará al método EvadeAction mientras que el 
     * agente se encuentre en ese estado.
     */
    IEnumerator Evade()
    {
        while (currentState.Equals(state.Evade))
        {
            EvadeAction();
            yield return 0;
        }
    }

    /*
     * FrogIn: corrutina del estado FrogIn que llamará al método FrogInAction mientras que la 
     * rana se encuentre en ese estado.
     */
    IEnumerator FrogIn()
    {
        while (currentState.Equals(state.FrogIn))
        {
            FrogInAction();
            yield return 0;
        }
    }


    /*
     * FrogOut: corrutina del estado FrogIn que llamará al método FrogOutAction mientras que la 
     * rana se encuentre en ese estado.
     */
    IEnumerator FrogOut()
    {
        while (currentState.Equals(state.FrogOut))
        {
           FrogOutAction();
            yield return 0;
        }
    }
    #endregion Coroutines
}
