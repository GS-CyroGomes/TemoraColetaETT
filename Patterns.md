
***MVVM***
	- **Model** -> Regra de negocio
	- **Views** -> Interface da aplicação captura de informações,
		- Alerta de erros
		- Mostrar informações
		- Captura de informações do usuario
		- Carregar dados para usuario
		- Gerencia de estados da aplicação
	- **ViewModel** -> Interpretar informações do usuário e preparar informações que a Vies Renderiza
		- 
***CleanArchitecture***
	- **Presentation** -> Camada de UI (User Interface)
	- **Domain** -> Regra de negocio
	- **Data** -> Informações e dados da aplicação
		-  Banco de dados
		-  Redes
		-  Remore config (Firebase ou ww)
***CleanArchitecture + MVVM***
	- **Presentes** (***CleanArchitecture***)
		- ***View** == * Observer (***MVVM***)
			1. Bind viewModel
			2. Observar uma variavel que está no ViewModel
		- ***ViewModel*** == * Observable : (***MVVM***)
			1. Devolve uma Notificação (*observable*) para a View
			2. Quando acontece uma alteração numa variável o View é notificado
			3. Quando necessita executar uma regra de negócio envia as informações para a camada **Domain**.
		Observer - Observavel => - Quem recebe notificações de mudanças.
		Observable - Observador => - Quem emite eventos, sofre mudanças ou moda de estado, que interessam ao Observador.
		Exemplo: Sobreposição quântica
			- **Observable (observável quântico):**  
				1. É a propriedade que pode ser medida — por exemplo, posição, momento, spin.
			- **Observer (observador / medidor):**  
				1. É o sistema de medição (não necessariamente uma pessoa, pode ser um detector).	    
			- **Sobreposição:**  
				1. Antes da medição, o estado quântico está em **superposição** (várias possibilidades coexistindo).  
				2. Quando o **observador mede** o **observável**, o estado colapsa para um valor específico.
	- **Domain** (***CleanArchitecture***)
		1. É acionada quando o ViewModel necessita executar alguma regra de negócio.
		2. Caso necessite de alguma informação para executar a regra de negocio aciona a damada **Data**
	- **Data** (***CleanArchitecture***)
		1. Responsável por **persistência, APIs externas, banco, cache**.
	- Exemplo Server Request:
		1. Passamos as camadas **Data**
		2. A camada **Data** monta o objeto de *request* para o servidor
		3. A camada **Data** recebe o objeto de *response* do servidor
		4. A camada **Data** mapeia o objeto de *reponse* para um objeto que a camada **Domain** conheça.
			1. A Camada **Domain** não deve conhecer nada que há na camada **Data**, **View** ou **ViewModel**.
			2. Caso o servidor ao qual estão sendo feitas as *requests*, e o objeto de *response* for diferente, a unica modificação a ser feita em nossa arquitetura será no mapeamento de objetos entre a camada **Data** e a camada **Domain**.
			3. Mantendo assim a regra de negocio funcionando.
		5. Executa métodos necessários para enviar o objeto para o **ViewModel**.
		6. Interpreta o resultado que recebido da camada **Domain** e define o próximo estado da aplicação, armazena em uma variável.
		7. A variável que armazena o prximo estado da aplicação está sendo observada pela camada **View**, assim a mesma atualiza o seu estado.

Tarefas 24/09
- [x] Criar estrutura de pastas
- [ ] Criar Objetos C#
- [x] Criar camada comunicação com a api
- [ ] Criar e estabelecer comunicação com bando local sqlite
- [ ] Dockerizar camadas Data & Domain

src/Core/TemoraColetaETT.Domain
src/Core/TemoraColetaETT.Application
src/Infrastructure/TemoraColetaETT.Persistence # Para o SQLite
src/Infrastructure/TemoraColetaETT.ApiClient   # Para a API Externa