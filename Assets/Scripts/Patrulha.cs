using UnityEngine;
using UnityEngine.AI;

public class Patrulha : MonoBehaviour
{
    public Transform[] pontosPatrulha; // Pontos de patrulha
    private int pontoAtual = 0; // Índice do ponto atual
    private NavMeshAgent navMeshAgent; // Agente de navegação
    public Transform player; // Referência ao jogador
    public float distanciaPerseguicao = 10f; // Distância para começar a perseguição
    public float distanciaPatrulha = 25f; // Distância para retomar a patrulha

    void Start()
    {
        // Obtém o NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Verifica se há pontos de patrulha definidos
        if (pontosPatrulha.Length > 0)
        {
            navMeshAgent.SetDestination(pontosPatrulha[pontoAtual].position);
        }
    }

    void Update()
    {
        // Verifica a distância entre o inimigo e o jogador
        float distanciaParaJogador = Vector3.Distance(transform.position, player.position);

        if (distanciaParaJogador <= distanciaPerseguicao)
        {
            // Inicia a perseguição ao jogador
            PerseguirJogador();
        }
        else if (distanciaParaJogador > distanciaPatrulha)
        {
            // Retorna à patrulha se o jogador estiver longe
            Patrulhar();
        }
    }

    // Método para seguir o jogador
    void PerseguirJogador()
    {
        navMeshAgent.SetDestination(player.position); // Faz o inimigo ir até o jogador
    }

    // Método para voltar a patrulhar
    void Patrulhar()
    {
        if (navMeshAgent.remainingDistance < 1f && !navMeshAgent.pathPending)
        {
            // Muda para o próximo ponto de patrulha
            pontoAtual = (pontoAtual + 1) % pontosPatrulha.Length;
            navMeshAgent.SetDestination(pontosPatrulha[pontoAtual].position);
        }
    }
}