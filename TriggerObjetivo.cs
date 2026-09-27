using UnityEngine;

public class TriggerObjetivo : MonoBehaviour
{
    public int idObjetivo; // Identificador do objetivo associado a este cubo

    private Objetivos Uiobjetivos;
    void Start()
    {
        GameObject obj = GameObject.Find("ObjetivosExecutar");
        if (obj != null)
        {
            Uiobjetivos = obj.GetComponent<Objetivos>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Uiobjetivos != null && Uiobjetivos.objetivoAtual == idObjetivo)
            {
                AudioSource coletaLivros = GetComponent<AudioSource>();
                if (coletaLivros != null && coletaLivros.clip != null)
                {
                    AudioSource.PlayClipAtPoint(coletaLivros.clip, transform.position);
                }

                Uiobjetivos.CompletarObjetivo();
                //Se quiser que o objeto suma ao encostar nele adicione o comando abaixo:
                Destroy(gameObject);
            }

        }
    }

}
