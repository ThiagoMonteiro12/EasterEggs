using UnityEngine;
using UnityEngine.AI;

public class Patrulha : MonoBehaviour
{
    public Transform[] pontosPatrulha; // Pontos de patrulha
    private int pontoAtual = 0; // �ndice do ponto atual
    private NavMeshAgent navMeshAgent; // Agente de navega��o
    public Transform player; // Refer�ncia ao jogador
    public float distanciaPerseguicao = 10f; // Dist�ncia para come�ar a persegui��o
    public float distanciaPatrulha = 25f; // Dist�ncia para retomar a patrulha

    void Start()
    {
        // Obt�m o NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Verifica se h� pontos de patrulha definidos
        if (pontosPatrulha.Length > 0)
        {
            navMeshAgent.SetDestination(pontosPatrulha[pontoAtual].position);
        }
    }

    void Update()
    {
        // Verifica a dist�ncia entre o inimigo e o jogador
        float distanciaParaJogador = Vector3.Distance(transform.position, player.position);

        if (distanciaParaJogador <= distanciaPerseguicao)
        {
            // Inicia a persegui��o ao jogador
            PerseguirJogador();
        }
        else if (distanciaParaJogador > distanciaPatrulha)
        {
            // Retorna � patrulha se o jogador estiver longe
            Patrulhar();
        }
    }

    // M�todo para seguir o jogador
    void PerseguirJogador()
    {
        navMeshAgent.SetDestination(player.position); // Faz o inimigo ir at� o jogador
    }

    // M�todo para voltar a patrulhar
    void Patrulhar()
    {
        if (navMeshAgent.remainingDistance < 1f && !navMeshAgent.pathPending)
        {
            // Muda para o pr�ximo ponto de patrulha
            pontoAtual = (pontoAtual + 1) % pontosPatrulha.Length;
            navMeshAgent.SetDestination(pontosPatrulha[pontoAtual].position);
        }
    }
}