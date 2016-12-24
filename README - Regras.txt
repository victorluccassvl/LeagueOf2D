***Memória < processamento ( PRIORIDADE PARA  CLAREZA)
***NAO CRIAR CLASSES INUTEIS

0 - Diretrizes

	0.1) Optar por uso excessivo de memória ao invés de perda de processamento

	0.2) Prioridade para clareza acima de tudo

	0.3) Não criar classes sem necessidade real

1 - Detalhes visuais de código

	1.1) Chaveamento {} alinhado verticalmente

		Exemplo:
		loop/função ( param )
		{
			código
		}

	1.2) Com ' ' entre if/for e abertura de parenteses '('
		 Com ' ' antes e depois de '==' e '='
		 Com ' ' entre nome da função e abertura de parenteses '(' na declaração
		 Sem ' ' entre nome da função e abertura de parenteses '(' na chamada
		 Sem ' ' entre '(' e conteúdo seguinte
		 Sem ' ' entre ')' e conteúdo anterior
		 ';' e ',' colados no conteúdo da esquerda
		 Com ' ' após ';' e ','

		 Exemplo:
		 int função (char param1, float param2)
		 {
	 		 for (i = 0; i != 5; i++)
	 		 {
				 return função(param1, param2);
			 }
		 }

	1.3) Uso de Underline em nomes de variáveis
		 Uso de camelCase em nomes de funções
		 Uso de camelCase com primeira letra maiúscula em nomes de classes
		 Uso de '-' em nomes de arquivos
		 Uso do formato a seguir para funções de teste

		 Exemplo:
		/**
		 * This function prints hello world [x] times.
		 *
		 * :x: number of times to print hello world.
		 * :return: nothing
		 */
		printHelloWorldXTimes(int x)
		{
			Console.debug("Hello World!");
		}


	1.4) Não utilizar if/for sem chaves, mesmo que tenha apenas um comando

		Contra-Exemplos:
		if (fechado == true)
			printf("oi");
		if (fechado == true) printf("oi");

	1.5) Utilizar 3 linhas brancas espaçando implementação de funções

	1.6) Identação com 4 ' ' para cada nível

	1.7) Limite de 120 caracteres por linha.
		 Em caso de overflow devido a excesso de parâmetros, alinhar a linha de baixo com o '('

	1.8) Todo conteúdo textual será em Ingles

2 - Comentários

	2.1) Comentário obrigatório em todo bloco (for, if, vazio, etc)

	2.2) Comentário com '/**/' para cabeçalho de funções, classes e construtores

	2.3) Comentário com '//' para todas as outras situações. Utilizando um em cima do outro para múltiplas linhas

		 Exemplo:
		 // primeira linha
		 // segunda linha

	2.4) Comentário sempre diretamente na linha acima ao bloco ou comando, com mesma identação

	2.5) Comentar chamadas de funções explicando parâmetros e retorno sempre que não for óbvio seu funcionamento

	2.6) Commit bem comentado e explicado

		Exemplo:

		Mudanças:
		- Corrigi um erro onde "Hello World!" era printado como "Helo Word!".
		- Corrigi um erro onde a função de soma não funcionava para números negativos.
		- Otimizei a função "inverseSquareRoot".

	2.7) Seguir o padrão a seguir para descrição de classes e métodos :

		Exemplo:
		/**
		 * This is a projectile, it can collide and/or deal damage to a character.
		 */
		class Projectile
		{
			public int damage;
			public float time_to_live;

			/**
			 * :damage: the amount of damage this projectile does.
			 * :ttl: time to live in seconds (after this time the projectile disappears).
			 */
			public Projectile(int damage, float ttl)
			{
				this.damage = damage;
				this.time_to_live = ttl;
			}

			/**
			 * Time to live defaults to 1 minute.
			 *
			 * :damage: the amount of damage this projectile does.
			 */
			public Projectile(int damage)
			{
				this.damage = damage;
				this.time_to_live = 60;
			}

			/**
			 * Damage defaults to 0.
			 * Time to live defaults to 1 minute.
			 */
			public Projectile()
			{
				this.damage = 0;
				this.time_to_live = 60;
			}
		}

3 - Detalhes técnicos

	3.1) Permitido o uso de break

	3.2) Permitido o uso de continue

	3.3) Permitido o uso de goto ( Apenas para avançar linhas, nunca retroceder )

	3.4) Permitido o uso de múltiplos returns dentro de uma função

	3.5) Permitido o uso de enum

	3.6) Não lançar exceptions ( a não ser que alguma função da biblioteca padrão te obrigue ). Tratar erros com if e chamada da função error

	3.7) Criar métodos de teste para funções grandes e essenciais

	3.8) Utilizar blocos vazios para evitar uso de funções usadas apenas uma única vez
		 Utilizar blocos vazios para evitar encadear muita informação em apenas uma linha

		 Obs: Um bloco vazio consiste na abertura e fechamento de chaves {}, criando um bloco com escopo próprio

		 Exemplo de uso para a primeira situação citada :

		 // Inicialização da matriz
		 {
			int i,j;
			for(i = 0; i<linhas; i++)
			{
				for(j = 0; j<colunas; j++)
				{
					matriz[i][j] = 0;
				}
			}
		 }

		 Note que nesse exemplo as variáveis i e j serão criadas nesse escopo do bloco, e logo após o bloco fechar elas irão sumir
		 Note que o uso desse artificio permite minimizar o bloco inteiro pelo recurso dos editores de texto

		 Exemplo de uso para a segunda situação citada :
	
		 Note o seguinte exemplo de chamada :
			 Soup.make(pato.getHead().getBico(), frog.getLeg())

		 Ele pode ser fragmentado no seguinte bloco :

		 // Fazer sopa com perna de sapo e bico de pato
		 {
			Head head = pato.getHead();
			Bico bico = head.getBico();
			Leg leg = frog.getLeg();

			Soup.make(bico, leg);
		 }

		 Note que nesse exemplo ganha-se clareza e as variáveis criadas são rapidamente liberadas pois pertencem ao escopo do bloco