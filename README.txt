SETTING UP


--- DATABASE ---

Requirements: Microsoft SQL Server Management Studio 18
There are two ways: using the .sql file, using the .bacpac file

Using the .bacpac file
1. Follow this video from timestamp 0:41 to import database (https://www.youtube.com/watch?v=QdKOqlD_3jw)
2. When importing, use the file by the name of VirtualLibraryDB.bacpac
3. In appsettings.json file in Visual Studio Solution, enter database connection string under the heading
	Enter your connection string here:
	"AppDbConnection": 

Using the .sql file
1. Follow this video (https://www.youtube.com/watch?v=AJdwp0fAHf4)
2. When importing, use the file by the name of VirtualLibraryDB.sql
3. In appsettings.json file in Visual Studio Solution, enter database connection string under the heading
	Enter your connection string here:
	"AppDbConnection":



--- MAILTRAP ---

There are two ways: create new account, use provided account.
(Mailtrap is a free web service where you can register new account where you only need your email)


If it is decided to create new account, follow these steps:
1. Create account: (https://mailtrap.io/register/signup?ref=header)
2. Click on inbox that has been created for you
3. In the center of the screen, there should be a drop-down link called "Show Credentials"
4. Once clicked, copy only the UserName and Password under the SMTP card and paste both in the appsettings.json 
in Visual Studio Solution under "SMTPConfig" where there are specified fields for both
(the existing username and password are there for the already created account specified earlier)

If it is decided to use the account specified earlier, the following are the login details for this account
	email: k00246756.pyatnychka.fyp@gmail.com
	password: k00246756!pyat!FYP!Mail
There is no need to change anything in code for this



--- STRIPE ---

There are two ways: create new account, use provided account.
(Stripe is free to sign up)

If it is decided to create new account, follow these steps:
1. Create new account: (https://dashboard.stripe.com/register)
2. Once account has been created and email has been verified, go to dashboard: (https://dashboard.stripe.com/test/dashboard)
3. Towards the right of the screen you will see there are two keys (publishable key and secret key)
4. Copy and paste into appsettings.json file in Visual Studio Explorer under the headings for "Stripe":
	"SecretKey":
	"PublishableKey":


Use provided account:
	email: k00246756.pyatnychka.fyp@gmail.com
	password: k00246756!pyat!FYP!Stripe


This is the end for the Stripe setup.
When you have run the application, and click Pay by Card button to start a new membership, Stripe provides
test cards for us to use:

**Please note that the keys are not live keys but are only test keys for the use of testing so there is no real money or real card details involved**

Test successful card: 
	Card Number: 4242 4242 4242 4242
	CVC: any three digit number
	Expiry Date: any future date

Test fail card:
	Card Number: 4000 0000 0000 0002
	CVC: any three digit number
	Expiry Date: any future date

For more details please visit: (https://stripe.com/docs/testing)



--- Finally ---
After all of the above has been setup, you may now run the Visual Studio application.