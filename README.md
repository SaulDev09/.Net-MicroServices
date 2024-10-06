# .Net-MicroServices

Azure | SQL Server

### Basics
Subscription: Pay as you go
Resource group: udemy-mastery

Database name: SC_Auth
Server: Create new => 
		Server name: sc-udemy-mastery
		Location: (US) East US (Default)

	Authentication method: Use SQL authentication
		Server admin login / Password

Want to use SQL elastic pool?: No
Workload environment: Development

Compute + storage: 
	Service tier: Basic (For less demanding workloads) => Estimated cost / month: 4.90 USD

Backup storage redundancy: Locally-redundant backup storage (Default)

### Network connectivity
Connectivity method: Public endpoint
Allow Azure services and resources to access this server: Yes
Add current client IP address: Yes

Connection policy: Default

### Security: Default
Enable Microsoft Defender for SQL: Not now (Default)
Ledger: Not configured
Server identity: Not enabled
Server level key: Service-managed key selected
Database level key: Not configured
Always encrypted: OFF

### Additional settings: Default
Data source | Use existing data: None
Database collation | Collation: SQL_Latin1_General_CP1_CI_AS
Maintenance window: System default (5pm to 8am)

### Tags: Default


Azure | App Services from Visual Studio
SC.Services.AuthAPI > right click > Publish > Azure, Next > Azure App Service (Linux), Next > Login
TAB App Service > Create new > 
	Name: SCServicesAuthAPI (without datetime)
	Subscription name: Pay as you go
	Resource group: udemy-mastery (same as DB)
	Hosting Plan: 
		SCServicesAuthAPIPlan
		Location: East US
		Size: Free F1 (Shared Infrastructure)
		OK
	CREATE
	NEXT
TAB API Management > Skip this step > NEXT > 
TAB Deployment type > Publish (generates pubxml file) (default) > FINISH

Publish window:
	More actions > Edit (pencil)
		Deployment Mode: Self-Contained > Save > 
	PUBLISH

Go to Azure Portal > App Services > Settings > Environment variables > Add > 
	Add/Edit application setting
		Name: ASPNETCORE_ENVIRONMENT
		Value: Production
		OK > Apply

To be "Always on" (Prevents your app from being idled out due to inactivity) for RewardAPI and EmailAPI:
Go to Azure Portal > App Services > Settings > 
	Scale up (App Service plan) > Basic B1 > Select
	Configuration > "Always on"
	


