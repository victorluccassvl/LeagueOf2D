***Mem�ria < processamento ( PRIORIDADE PARA  CLAREZA)
***NAO CRIAR CLASSES INUTEIS

0 - Diretrizes

	0.1) Optar por uso excessivo de mem�ria ao inv�s de perda de processamento

	0.2) Prioridade para clareza acima de tudo

	0.3) N�o criar classes sem necessidade real

1 - Detalhes visuais de c�digo

	1.1) Chaveamento {} alinhado verticalmente

		Exemplo:
		loop/fun��o ( param )
		{
			c�digo
		}

	1.2) Com ' ' entre if/for e abertura de parenteses '('
		 Com ' ' antes e depois de '==' e '='
		 Com ' ' entre nome da fun��o e abertura de parenteses '(' na declara��o
		 Sem ' ' entre nome da fun��o e abertura de parenteses '(' na chamada
		 Sem ' ' entre '(' e conte�do seguinte
		 Sem ' ' entre ')' e conte�do anterior
		 ';' e ',' colados no conte�do da esquerda
		 Com ' ' ap�s ';' e ','

		 Exemplo:
		 int fun��o (char param1, float param2)
		 {
	 		 for (i = 0; i != 5; i++)
	 		 {
				 return fun��o(param1, param2);
			 }
		 }

	1.3) Uso de Underline em nomes de vari�veis
		 Uso de camelCase em nomes de fun��es
		 Uso de camelCase com primeira letra mai�scula em nomes de classes
		 Uso de '-' em nomes de arquivos
		 Uso do formato a seguir para fun��es de teste

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


	1.4) N�o utilizar if/for sem chaves, mesmo que tenha apenas um comando

		Contra-Exemplos:
		if (fechado == true)
			printf("oi");
		if (fechado == true) printf("oi");

	1.5) Utilizar 3 linhas brancas espa�ando implementa��o de fun��es

	1.6) Identa��o com 4 ' ' para cada n�vel

	1.7) Limite de 120 caracteres por linha.
		 Em caso de overflow devido a excesso de par�metros, alinhar a linha de baixo com o '('

	1.8) Todo conte�do textual ser� em Ingles

2 - Coment�rios

	2.1) Coment�rio obrigat�rio em todo bloco (for, if, vazio, etc)

	2.2) Coment�rio com '/**/' para cabe�alho de fun��es, classes e construtores

	2.3) Coment�rio com '//' para todas as outras situa��es. Utilizando um em cima do outro para m�ltiplas linhas

		 Exemplo:
		 // primeira linha
		 // segunda linha

	2.4) Coment�rio sempre diretamente na linha acima ao bloco ou comando, com mesma identa��o

	2.5) Comentar chamadas de fun��es explicando par�metros e retorno sempre que n�o for �bvio seu funcionamento

	2.6) Commit bem comentado e explicado

		Exemplo:

		Mudan�as:
		- Corrigi um erro onde "Hello World!" era printado como "Helo Word!".
		- Corrigi um erro onde a fun��o de soma n�o funcionava para n�meros negativos.
		- Otimizei a fun��o "inverseSquareRoot".

	2.7) Seguir o padr�o a seguir para descri��o de classes e m�todos :

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

3 - Detalhes t�cnicos

	3.1) Permitido o uso de break

	3.2) Permitido o uso de continue

	3.3) Permitido o uso de goto ( Apenas para avan�ar linhas, nunca retroceder )

	3.4) Permitido o uso de m�ltiplos returns dentro de uma fun��o

	3.5) Permitido o uso de enum

	3.6) N�o lan�ar exceptions ( a n�o ser que alguma fun��o da biblioteca padr�o te obrigue ). Tratar erros com if e chamada da fun��o error

	3.7) Criar m�todos de teste para fun��es grandes e essenciais

	3.8) Utilizar blocos vazios para evitar uso de fun��es usadas apenas uma �nica vez
		 Utilizar blocos vazios para evitar encadear muita informa��o em apenas uma linha

		 Obs: Um bloco vazio consiste na abertura e fechamento de chaves {}, criando um bloco com escopo pr�prio

		 Exemplo de uso para a primeira situa��o citada :

		 // Inicializa��o da matriz
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

		 Note que nesse exemplo as vari�veis i e j ser�o criadas nesse escopo do bloco, e logo ap�s o bloco fechar elas ir�o sumir
		 Note que o uso desse artificio permite minimizar o bloco inteiro pelo recurso dos editores de texto

		 Exemplo de uso para a segunda situa��o citada :
	
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

		 Note que nesse exemplo ganha-se clareza e as vari�veis criadas s�o rapidamente liberadas pois pertencem ao escopo do bloco