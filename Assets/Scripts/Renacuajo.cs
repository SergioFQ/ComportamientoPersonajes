using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/*
 * Renacuajo: clase que hereda de BaseAgent para definir la característica flocking 
 * propias de los renacuajos que no disponen el resto de agentes.
 */
public class Renacuajo : BaseAgent
{


    //Campo para indicar el radio de separación entre renacuajos vecinos
    public float radioRenacuajosVecinos = 3f;
    public bool isLeader;
    public Renacuajo leader;
    public LayerMask renLayer;

    #region Functions
    /*
     * WanderAction: función que sobreescribe el comportamiento wander de BaseAgent para
     * incluir el comportamiento flocking en los renacuajos.
     */
    protected override void WanderAction()
    {

        Collider[] vecinos = Physics.OverlapSphere(transform.position, radioRenacuajosVecinos, renLayer);
        List<Renacuajo> renacuajos = vecinos.Select(o => o.GetComponent<Renacuajo>()).ToList();
        renacuajos.Remove(this);

        Vector2 dest = transform.position + transform.forward*_agent.speed + Random.insideUnitSphere*_wanderRadius;
        
        if (isLeader) 
        {
            _agent.SetDestination(dest);
            bool foundGroup = false;
            foreach (Renacuajo r in renacuajos)
            {
                if (r.isLeader)
                {
                    r.isLeader = false;
                    foundGroup = true;
                }
            }
            if (foundGroup) isLeader = false;
        }
        else
        {
            if (leader == null || leader.gameObject == null || Vector2.Distance(transform.position, leader.gameObject.transform.position) > maxPerceptionL)
            {
                bool foundNew = false;
                foreach (Renacuajo r in renacuajos)
                {
                    if (r.isLeader)
                    {
                        foundNew = true;
                        leader = r;
                    }
                }
                if (!foundNew)
                {
                    isLeader = true;
                    leader = null;
                    foreach (Renacuajo r in renacuajos)
                    {
                        if ((r.leader == null || r.leader.gameObject == null))
                        {
                            r.leader = this;
                        }
                    }
                }
                if (isLeader)
                    _agent.SetDestination(dest);
                else
                    _agent.SetDestination((leader.gameObject.transform.position - transform.position)/2 + transform.position + Random.insideUnitSphere*Vector2.Distance(leader.gameObject.transform.position,transform.position)/2);

            }
            else
            {
                _agent.SetDestination((leader.gameObject.transform.position - transform.position)/2 + transform.position + Random.insideUnitSphere*Vector2.Distance(leader.gameObject.transform.position,transform.position)/2);
            }
        }
    }
    #endregion Functions
}
