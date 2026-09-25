using UnityEngine;
using System.Collections;

public class PocaoCongelamento : MonoBehaviour
{
    [Header("Configurações do Congelamento")]
    public float tempoDeCongelamento = 4f;
    public float raioDaExplosao = 3f;

    [Header("Configurações se Errar o Alvo")]
    [SerializeField] private float tempoAteSumirNoChao = 4f;
    
    private bool jaAtingiu = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (jaAtingiu) return;

        Transform objeto = collision.transform;
        bool acertouInimigo = false;

        while (objeto != null)
        {
            if (objeto.CompareTag("Inimigo"))
            {
                acertouInimigo = true;
                break;
            }
            objeto = objeto.parent;
        }

        jaAtingiu = true;

        if (acertouInimigo)
        {
            ExplodirPocao();
        }
        else
        {
            Destroy(gameObject, tempoAteSumirNoChao);
        }
    }

    private void ExplodirPocao()
    {
        EfeitoEmArea();
        Destroy(gameObject);
    }

    private void EfeitoEmArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, raioDaExplosao);

        foreach (Collider col in colliders)
        {
            Transform objeto = col.transform;

            while (objeto != null)
            {
                if (objeto.CompareTag("Inimigo"))
                {
                    // Comunica diretamente com o script que está no seu inimigo
                    AtaqueInimigo inimigo = objeto.GetComponent<AtaqueInimigo>();
                    
                    if (inimigo != null)
                    {
                        inimigo.Congelar(tempoDeCongelamento);
                    }
                    
                    break; 
                }
                objeto = objeto.parent;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, raioDaExplosao);
    }
}
