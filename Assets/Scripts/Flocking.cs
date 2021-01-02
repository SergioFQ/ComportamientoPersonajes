using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    //Variables que determinan las tres reglas del Flocking y sus correspondientes rangos para los sliders

    [Range(0, 3)]
    public float separacion = 0.1f;

    [Range(0, 3)]
    public float cohesion = 0.71f;

    [Range(0, 3)]
    public float alineamiento = 0.82f;


    public Vector2 aceleracion;
    public Vector2 velocidad;


    //Campo para indicar el radio de separación entre renacuajos vecinos
    [Range(0, 3)]
    public float angulo = 1.5f;

    //Rango para el slider que determina la velocidad máxima de los renacuajos
    [Range(0, 10)]
    public float maxVel = 6.44f;

    //Rango para el slider que determina la fuerza de dirección de los renacuajos
    [Range(.1f, .5f)]
    public float maxFuer = .0327f;

    //Campo para indicar el radio de separación entre renacuajos vecinos
    [SerializeField]
    public float radioRenacuajosVecinos = 3f;

    private Vector2 Posicion
    {
        get
        {
            return gameObject.transform.position;
        }
        set
        {
            gameObject.transform.position = value;
        }
    }

    private void Start()
    {   
        //Inicializacion del vector velocidad de los renacuajos, basada en el angulo dado
        velocidad = new Vector2(Mathf.Sin(angulo), Mathf.Cos(angulo));

        //Inicialización de la rotacion inicial de los renacuajos, basada en el angulo dado
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));
    }

    private void Update()
    {
        //Detectamos los renacuajos vecinos en el radio dado
        Collider2D[] vecinos = Physics2D.OverlapCircleAll(Posicion, radioRenacuajosVecinos);
        List<Flocking> renacuajos = vecinos.Select(o => o.GetComponent<Flocking>()).ToList();
        renacuajos.Remove(this);
       
        //Aplicamos el Flocking
        Flock(renacuajos);

        //Actualizamos la velocidad, posicion y rotacion del renacuajo
        actualizarVelocidad();
        actualizarPosition();
        actualizarRotacion();
    }

    //Metodo para aplicar el Flocking
    private void Flock(IEnumerable<Flocking> renacuajos)
    {
        //Obtenemos los valores clave
        Vector2 alin = Alineacion(renacuajos);
        Vector2 separ = Separacion(renacuajos);
        Vector2 cohes = Cohesion(renacuajos);

        //Recalculamos el vector de aceleración teniendo en cuenta los valores obtenidos
        aceleracion = alineamiento * alin + cohesion * cohes + separacion * separ;
    }

    //Metodo para actualizar la velocidad
    public void actualizarVelocidad()
    {
        velocidad += aceleracion;
        
        //Limitamos con la velocidad máxima si fuera necesario
        if (velocidad.sqrMagnitude > (maxVel * maxVel))
        {
            velocidad = velocidad.normalized * maxVel;
        }
    }

    //Metodo para actualizar la posicion
    private void actualizarPosition()
    {
        //Empleamos Time.deltaTime para obtener movimiento constante
        Posicion += velocidad * Time.deltaTime;
    }

    //Metodo para actualizar la rotacion
    private void actualizarRotacion()
    {
        //Obtenemos un angulo en grados segun el vector velocidad y lo aplicamos a la rotacion del renacuajo
        float ang = Mathf.Atan2(velocidad.y, velocidad.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, ang));
    }

    //Metodo de la separacion del Flocking
    private Vector2 Separacion(IEnumerable<Flocking> vecinos)
    {
        Vector2 sep = Vector2.zero;

        //Detecta los renacuajos vecinos que se encuentran cerca
        vecinos = vecinos.Where(o => Distancia(o) <= radioRenacuajosVecinos / 2);

        //Si no encontramos vecinos no varia
        if (vecinos.Count() == 0)
        {
            return sep;
        };

        //Si encontramos vecinos obtenemos la diferencia de posiciones y hacemos la media
        foreach (var vecino in vecinos)
        {
            Vector2 diferencia = Posicion - vecino.Posicion;
            sep += diferencia.normalized / diferencia.magnitude;
        }
        sep /= vecinos.Count();

        //Obtenemos el nuevo vector
        Vector2 vec = Direccion(sep.normalized * maxVel);
        return vec;
    }

    //Metodo para calcular la distancia de los vecinos cercanos
    private float Distancia(Flocking vecino)
    {
        return Vector3.Distance(vecino.transform.position, Posicion);
    }

    //Metodo de la alineacion del Flocking
    private Vector2 Alineacion(IEnumerable<Flocking> vecinos)
    {
        Vector2 vel = Vector2.zero;

        //Si no encontramos vecinos no varia
        if (vecinos.Count()==0)
        {
            return vel;
        };

        //Si encontramos vecinos hacemos la media
        foreach (var vecino in vecinos)
        {
            vel += vecino.velocidad;
        }
        vel /= vecinos.Count();

        //Obtenemos el nuevo vector
        Vector2 vec = Direccion(vel.normalized * maxVel);

        return vec;
    }


    //Metodo de la cohesion del Flocking
    private Vector2 Cohesion(IEnumerable<Flocking> vecinos)
    {
        Vector2 coh = Vector2.zero;

        //Si no encontramos vecinos no varia
        if (vecinos.Count() == 0)
        {
            return coh;
        };

        //Si encontramos vecinos hacemos la media
        foreach (var vecino in vecinos)
        {
            coh += vecino.Posicion;
        }
        Vector2 media = coh / vecinos.Count();


        //Obtenemos el nuevo vector
        Vector2 nuevaDirec = media - Posicion;
        Vector2 vec = Direccion(nuevaDirec.normalized * maxVel);

        return vec;
    }


    private Vector2 Direccion(Vector2 vec)
    {
        //Calculamos el nuevo vector de direccion y limitamos con la fuerza máxima si fuera necesario
        Vector2 dir = vec - velocidad;
        
        if (dir.sqrMagnitude > (maxFuer* maxFuer))
        {
            return dir.normalized * maxFuer;
        }
        else
        {
            return dir; 
        }
      
    }

}
