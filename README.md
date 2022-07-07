# MyFinalProject
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177803906-fbafd3f7-96df-470f-bc40-82b08739f2f5.png)


</br></br>
N-Tier architecture is used in this project. The reasons for using N-Tier architecture are easy project management, debugging, application of rules. On the other hand, since each layer has its own responsibilities, it is to write understandable, sustainable, clean, and understandable code. Let's explain the layers in general terms. Basically, only DataAccess, Business and WebApi layers are required. Other layers can be added optionally. There are 2 extra layers in this Project, Core and Entity layers.

Entity Layer:

In this layer, objects representing the tables in the database are kept, also called Entity. In addition to these, there are DTO (Data Transfer Objects) to keep the objects resulting from the combination of the tables.

Core Layer:
The core layer is the common layer of all other layers. It is to include the processes that affect the overall project and that we need at each layer. For example: Custom Exceptions, Middleware’s, Interceptors, JWT, Hashing, Generic repositories for database operations, Caching operations, Validation Tools, Authority management Tools, Response Models also known as Wrappers, File upload helper, etc. The common tool needs of other layers are kept in this layer.

Data Access Layer:
	
In this layer, after all validations, business rules and authorization checks are done, adding, updating, deleting and listing operations to the database are performed. In other words, a transaction is strictly implemented in the Data Access layer if it has been approved by the Business Layer. Dependency Inejction and switching between objects (Mapping with AutoMapper) are done here.

Business Layer:
Here, various operations are performed on the data coming from the Web API layer. These operations are respectively controlled by the user's authority to perform this operation, checking the validation rules of the incoming data, applying the business rules and restricting the data. After all these rules are ok, the Business Layer reaches the Data Access layer and performs the database operation.

WebApi Layer:
This layer is the first layer that receives the request from the front-end. Restful architecture is used as web API for this project. The biggest reason for using Restful is that it is flexible. Data from the user leads directly to the Business tier. If the result in the business layer is successful, a response with a 200 message is sent back, if not, a response as 400 bad request is returned.

Layer Details

Web API Layer

Requests from the client (for CRUD operations) are first met at the Web Api layer. The request sent by the user is directed to the Controller according to the path from the URL. The controller applies to the Business layer with this information. If the request from the Business layer is successful, the user is sent a response with "Ok()", that is, 200 status code. Otherwise, a response is sent with “BadRequest()” that is 400 status code.

















For example, the user wants to list the Case Types. A request is sent to the server with the URL shown in Figure B-1.1. The result of the request is as in Figure B-1.2.
</br></br>	
![image](https://user-images.githubusercontent.com/42031794/177803992-04cb9512-5759-4a31-ad2d-e329880fcd09.png)
		</br></br>
Figure: B-1
</br></br>
While this is the image on the client side, let's examine how things are going on the server.
Let's start from the Url in (Figure: B-1.1). When we remove the Domain name in the Url, the remaining is "api/CaseTypes/GetAll". The "CaseTypes" here corresponds to the "CaseTypesController" class in Figure: B-2.1. As seen in Figure: B-2.3, "GetAll" is a method that returns the CaseTypes of this class. Figure: In B-2.2, two services "Constructor Injection" were made. Thanks to the "ICaseeService", the Business layer is accessed. Thanks to the "GetAll" method belonging to the "ICaseeService", the desired data is obtained from the Business layer.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804007-5dce0b26-af1b-4b7f-92c6-73ba6b27bf1d.png)
   </br></br>
Figure: B-2
</br></br>
Swagger was used while developing this project. An important purpose of Swagger is to provide an interface for RestApi. This allows both back-end developers and front-end developers to see, review, test and understand the features of RestApi. "Authorize" option has been added for transactions that require authorization. Figure: B-3 As can be seen, a "JsonWebToken" (JWT) comes after the login process is completed.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804073-d7cc2504-dba0-4685-9c93-0ffbc7c7b3dc.png)
</br></br>
Figure: B-3
</br></br>
As seen in Figure:B-4, all tests can be performed after typing "Bearer" + our token in the "Value" input field in the model that opens after clicking the "Authorize" button. This number can be tested in the operations to be performed according to the user's authorizations.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804098-deafcdf5-e553-4ae0-a699-92a4299972ef.png)
</br></br>
Figure:B-4
</br></br>










Business Layer:

In this layer, Validation, Business Rules, Authority Control, Mapping between Objects, Cache, Performance Management and Dependency Injection operations are performed. The Business layer acts as a bridge from the WebApi layer to the DataAccess layer. If it is provided with the rule mentioned at the beginning, DataAccess layer can be accessed, and CRUD operations can be performed. Otherwise, the process returns to the WebApi layer without ever entering the DataAccess layer. 














</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804126-283e4e9c-5d6f-4f61-bfee-504f2c27f2ab.png)

</br></br>
Figure:B-5
</br></br>
As seen in Figure: B-5, there are 8 files in total in the Business layer.
In the Abstract folder, the interfaces of the Services (Figure: B-6) are created to reach the Business layer from the WebApi layer or to make the transactions loosely connected between the Services. Then, with the Dependency Injection method, these interfaces are accessed in the WebApi layer or in different services in the same layer. As it is known, interfaces are the signature of a Class, they cannot operate on their own. Dependency Injection allows the Classes that these interfaces are signed to be made into objects and used. In other words, create an instance from a class.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804139-1aa73100-4ef2-42e0-a98e-9070fac1afc1.png)
</br></br>
 Figure:B-6
</br></br>
Interfaces created in the Abstract folder are implemented into the Classes created in the Concrete folder, as seen in Figure B-7.1.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804154-0646608f-7815-4bec-9cb4-6b87b21d6be8.png)
 </br></br>
Figure:B-7
</br></br>
"[SecuredOperation("LicenceOwner,CaseTypeGetAll")]" Attribute seen in Figure:B-7.3 controls the user's authority to perform this operation. If the user has at least one of the "LicenceOwner" or "CaseTypeGetAll" privileges, he or she can perform this operation. The “SecuredOperation” class in the BusinessAspect folder extends “MethodInterception” (Figure: B-8). Thanks to this abstract class, it is possible to overwrite the methods as an Attribute. It can also be activated at any stage of the method. This is an Aspect Oriented Programming (AOP) approach. This issue is addressed in more detail in the Core layer. Figure: In B-8.1, the necessary permissions to run the method in the "_roles" variable are taken from the constructor. Then, within the "OnBefore" method, it is compared with the user's privileges on the "JWT" as in Figure:B-8.2. If the user has at least one of the authorizations required to run this method, this operation is performed.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804167-fb8a07a6-f0db-46cb-89f6-ff5cef49d46e.png)
    </br></br>
      Figure:B-8
</br></br>
Thanks to the "[ValidatiAspect(typeof(CaseTypeAddDtoValidator))" Attribute, which we also see in (Figure: B-7.6), the method applies Validation rules before it runs. “Fluent Validation” library is used for validation processes. As seen in Figure:B-9.2, the property can be validated with functions such as “GreaterThan”, “MinimumLength()” of the property selected thanks to the “RuleFor()” method. With the message we will write in the "WithMessage()" method, it is stated where the user made a mistake. If it is desired to write a special validation (Figure: B-9.3), a method that returns "bool" is written and the result is returned after the operation is done. This special validation method is written inside the "Must()" method to the "RuleFor()" method. Figure: “CaseTypeAddDto” seen in B-9.1 shows which object will be validated.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804207-2bb62e49-ae79-4b0d-ac1e-3c4a7d3a9eab.png)
 </br></br>
Figure:B-9</br></br>
Thanks to the "ICurrentUserService" that we see in Figure: B-7, the information of the user who is currently logged in is kept. As seen in Figure: B-10.1, "HttpContextAccessor" is obtained. Two methods have been written to obtain the LicenseId (Figure: B-10.2) or the UserId (Figure:B-10.3) of the logged in user. Then, this information was taken from the "JWT" and the encapsulation process was applied to prevent errors in Compile time. If the user wants to use this class without login (Figure: B-10.4), “UnauthorizedAccessException” is thrown back. The process of adding UserId and LicenseId to "JWT" is explained in detail in the Core layer.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804229-e15acc8c-c0fe-4042-b141-585f7ac300ef.png)
 </br></br>
Figure:B-10
</br></br>
When a request is made by the client, sometimes not all data is sent, some properties must be combined and a new property must be added. Automapper (Figure: B-7.5) is a simple library that allows mapping two objects to each other. It allows us to get rid of complex codes and save time. In other words, it is defined as a library that we use to map our Dto (Data Transfer Object) and Entity Models. As seen in Figure:B-11, it switches from the "CaseType" Entity to the "CaseTypeGetDto" Dto, and the transition settings are made in the constructor of the "AutoMapperMappingProfile" class.
</br></br>
 ![image](https://user-images.githubusercontent.com/42031794/177804268-92c9fcc3-b468-4601-81dd-85c90e177dc2.png)
</br></br>
Figure:B-11
</br></br>
The "Messages"(Figure:B12) class in Figure:B-7.7 is a static class. The messages we send to the user are here in a static form.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804289-665aa405-0d48-4059-b987-220f3861efce.png)
 </br></br>
Figure:B-12
</br></br>
IoC (Figure: B-13) (Inversion of Control) is a software development principle that aims to create objects with loose coupling throughout the lifecycle of the application. It is responsible for the life cycle of objects, provides their management. When an interface is injected into the class using IoC, the corresponding interface methods become available. Thus, the class using IoC only knows the methods it will use, even if there are more methods in the class, it will be able to access the methods specified in the interface. "Autofac" library was used while performing these operations.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804320-df947137-629d-406d-9394-fd5854f5a926.png)
 </br></br>
Figure:B-13</br></br>
The Singleton (Figure: B-13.1) pattern is used because there is no need to constantly create a new instance from the services when switching between layers.
</br></br>
Data Access Layer:
	
In the Data Access layer, CRUD    operations are performed directly on the database. Queries are written in this layer. Entity Framework, which is an ORM (Object Relation Mapping) framework, has been used. It is one of the Entity Framework ORM(Object Relational Mapping) tools.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804362-4a2b3e9d-9822-4316-b318-17a5fed9f54e.png)
</br></br>
Figure: B-14
</br></br>
What is ORM: It is a tool that acts as a bridge between a relational database and object-oriented programming (OOP). This bridge is a structure that uses our object models to manage our information in the relational database. In short, it is a framework that connects our objects to the database and exchanges data for us.
</br></br>
This project was developed with the Code First approach. Code First is a technique that connects the database and the programming language. It is an approach that enables the database operations in the project to be performed by writing code on the Visual Studio side as much as possible. Thanks to this approach, the relationship between the database interface and the software developer is minimized.
In the Code First structure, the classes with the IEntity signature in our project correspond to the "table" structures in the database, and the "property" structures correspond to the "column" structures in the database.
In this project, MSSQL, which is the Relational Database, was used. It is connected to the database thanks to the "LawContext" class in the Concrete folder (Figure: B-15.1).
Then, classes with IEntity signature are defined thanks to “DbSets”. The variables we introduced with “DbSet” (Figure: B-15.2) directly represent the database.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804377-a4342f9e-2b96-46dc-a5a3-87f69c005895.png)
 </br></br>
Figure:B-15
</br></br>
Then, in Figure: B-15.3, we establish the relationship between the tables from the database. For example, A "CaseType" can have only one "License", but a "License" can have more than one "CaseType".
After all Entities are defined, we come to the "Package Manager Console" and type "add-migration migration1" and run it. Then, after typing "update-database", the database and tables are created.
</br></br>
In this project, the "Repository Pattern" structure is used. It is to ensure the reusability of the codes created for our operations such as adding data to the database, updating and reading. “IEntityRepository<T>” is a generic interface. As seen in Figure: B-16.1, this generic interface can only be used for classes that have the "IEntity" interface. Thanks to this restriction, only the classes that represent the tables in our database can use this structure. Expression and Func (Figure: B-16.2) structures are used while writing the query. In this way, our queries are completed by typing Linq. In other words, it allows the data to be listed to be filtered while still in the database.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804396-e5eaad3d-187d-4e61-81ba-830a92560a8e.png)
 </br></br>
Figure:B-16
</br></br>
Since the "IEntityRepository" interface is not a real class, it must be implemented in another class. Thanks to the "EfEntityRepositoryBase" (Figure: B-17) class, database operations are performed generically with Entity Framework. The class “EfEntityRepositoryBase<TEntity,TContext>” (Figure:B-17.1) takes two generic structures. Only database-representing classes can use instead of "TEntity". That is, classes with the "IEntity" interface. "TContext", on the other hand, has to be used only for database operations. Figure: It is done in the process of adding to the database made in B-17.2. To do this, an instance must be created from "TContext". The "TContext" generic structure here can actually be thought of as "LawContext" (Figure: B-15). This instance created from "TContext" is a costly instance for the project as it provides database connection. Therefore, this operation is done between the "using()" blocks. Thanks to "using()", "TContext" is used in "using()" blocks, then "null" is assigned, and then "Carbeg Collector" deletes this instance consisting of "TContext". In other words, “IDisposable Pattern” is applied.
</br></br>
























</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804430-a50a5f0d-1ff5-4d8f-9b6d-6d6c0eca9925.png)
</br></br>




Figure: B-17
</br></br>
There are Interfaces in the Abstract folder. Thanks to these interfaces, Dependency Injection can be done, and database operations can be done through these interfaces from the Business layer.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804443-21a5ebb5-f4a5-40de-a546-b1978479cbe4.png)
 </br></br>
Figure:B-18
</br></br>
As seen in Figure:B-18.1, the "ICaseTypeDal" interface implements the "IEntityRepository<CaseType>" (Figure:B-17) interface. In "IEntityRepository<CaseType>", reading, adding, updating, deleting, data count and data control are already in a generic structure. The operations we want to add as an extra must be added separately, such as Figure: B-18.2.
</br></br>
Database operations can be performed using Entity Framework with the “EfCaseTypeDal” (Figure: B-19) found in the Concrete folder. The "EfCaseTypeDal" class implements the "EfEntityRepositoryBase" (Figure: B-17) class and the "ICaseDal" (Figure: B-18) interface. In this way, it can use the generic structures in "EfEntityRepositoryBase". “Ientity” is replaced by “CaseType”, “TContext” is replaced by “HukukContext”.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804464-9be592fa-809f-470b-a0e7-d1ce7da633b3.png)
 </br></br>
Figure:B-19
</br></br>
As seen in Figure:B-19, database operations are performed here, except for the "EfEntityRepositoryBase" class. We include the Entity in the Entity with the ".Include()" method that appears in Figure:b-19.1. It is provided to fetch the data from the tables to which it is connected.
</br></br>





Core Layer:
	
This layer contains the operations that affect the overall project. AOP (Aspect Oriented Programming) classes, Middlewares, Extensions, Hashing, Encryptin Custom Exceptions, File Helper, Result Models (Wrapper), Repository, JWT settings are written here.



</br></br>






![image](https://user-images.githubusercontent.com/42031794/177804494-2ca456e0-6178-4165-ab8c-c9ec30b10fd2.png)
</br></br>
Figure:B-20
</br></br>
Interceptors:
Interceptors intervene at certain points during method calls, allowing us to operate and manage our conflicting interests. Thus, we can perform some operations before or after the operation of the methods.
Let's start with the "MethodInterception" (Figure: B-21.1) class. Thanks to this class, it works in any case of a method (Before, After Exception or Success). Thanks to the “IInvocation invocation” found in the parameters of the methods, the information of the method being processed is obtained. Implemented from "MethodInterceptionBaseAttribute" (Figure: B-21.2). Thanks to the "MethodInterceptionBaseAttribute" abstract class, these operations can be done at Attribute and Interception levels.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804505-229db289-ea18-4d09-8b64-79c148715c8a.png)
 </br></br>
Figure:B-21
</br></br>
In this project, the "MethodInterception" class was used in the "SecuredOperation" class in the Business layer and the "ValidationAspect"(Figure: B-22) class in the Core layer. “SecuredOperation” was mentioned in the Business tier section of the report. 
![image](https://user-images.githubusercontent.com/42031794/177804524-69b30acd-77cb-4b8f-90d9-41e889cd3fe5.png)</br></br>

Figure:B-22
</br></br>
Thanks to the "ValidationAspect" class, user inputs are controlled with Fluent Validation. If the inputs entered by the user are valid, the system continues to work. Otherwise, "ValidationException" is thrown. The processes of writing validation rules are explained in the Business layer section of the report.
</br></br>
Response Models (Wrappers):
The results of the transactions made in the Business layer are sent to the Client side in a model (Figure: B-23). Every "bool Sucess" and "string Message" properties must be in every model. That is, what is inside the "IResult" interface. If data other than message and status will be sent to the user, a generic property named "T Data" should be added, such as "IDataResult<T>" interface. Concrete classes of these two interfaces are also written. Then, “ErrorResult”, “SuccessResult”, “ErrorDataResult<T>” and “SuccessDataResult<T>” were added to reduce “if-else” dependency. Thanks to these, “bool Success” will be assigned automatically. Error is false and Success is true.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804544-429c62ec-776b-47a1-be6a-e6a2b7475390.png)
 </br></br>
Figure:B-23
</br></br>
Security:
Thanks to the "HasgingHelper" (Figure: B-24) class, users can hash their passwords. Salt is used to make the hashed password to be added to the database more complex. To do this, a different hash value is added to the end of the password. In this way, while the security layer is created in the hashing process, especially brute force attacks are prevented.
</br></br>![image](https://user-images.githubusercontent.com/42031794/177804571-15b4a0d4-d3f9-4740-9a9b-3fc995de369e.png)
 </br></br>
Figure:B-24
</br></br>
A passwrod is hashed with the "CreatePasswordHash" method in "HashingHelper". "VerifyPasswordHash" checks the correctness of the entered password.
</br></br>
JWT:</br></br>
As seen in Figure:B-25, an "AccessToken" model is returned after the user logs in, thanks to the "CreateToken" method. Before that, there are the necessary processes for the formation of the "token". In order to perform an authorization check, the user's authorizations are put into the token (Figure: B-25.4). Then, the "Id" and "licenceId" of the user are put into the token (Figure: B-25.4).
With the "IConfiguration" interface, the JWT settings are taken from the "appsetting.json" (Figure: B-26) file. The jwt settings are made in the "JwtSecurityToken" (Figure: B-25.3) method with this information received. Some additions have been made in the "ClaimExtension" (Figure: B-27) class so that we can perform the operations in the "SetClaims" method. For example, adding and accessing “licenceId” (Figure: B-27.1) operations are created.


      




















</br></br>

![image](https://user-images.githubusercontent.com/42031794/177804605-550e9e3e-2c6a-4b2f-a03e-fb1574b454f5.png)
  </br></br>



Figure:B-25</br></br>
 ![image](https://user-images.githubusercontent.com/42031794/177804632-e53b7819-cc1d-421a-ba76-d68a2d5991cf.png)
</br></br>
Figure:B-26</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804647-a4f1a286-4a3e-4fd9-ba8c-ec5748431ebe.png)
</br></br>
 
Figure:B-27
</br></br>



ExceptionMiddleware:</br></br>
Middleware that comes with .NET Core are structures that allow us to manage the entire flow (application pipeline) in requests and responses in web applications. Each middleware can choose whether to pass the incoming request to the next middleware, it can run before or after the next middleware in the flow.











</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804692-55aaddc4-2f23-40c4-9838-5914a96e6c1a.png)
</br></br>
Figure:B-28
 	</br></br>
A simple middleware consists of a constructor that takes the RequestDelegate parameter and the Invoke method. “await next();” The request is executed with (Figure: B-28). We can perform the actions we want to do before and after.
In case of an Exception, the "HandlerExceptionAsync" (Figure: B-29) method works. As seen in Figure:B-29, there are 3 "Custom Exceptions". The first of these is the "ValidationException" from the Fluent Validation library. It is checked whether the incoming Exception is a ValidationException using Reflection (typeof). If there is an Exception related to Validation, the "if" block in Figure:B-29.1 will work. If the user has not approved the account, "Email" or "mobile phone number" Figure: The "if" block in B-29.2 works. If an operation is performed without user authorization, the "if" block in Figure:B-29.3 works. If these three are not Exceptions, it logs this operation because it is an exception related to the system. Figure:B-30.
</br></br>
![image](https://user-images.githubusercontent.com/42031794/177804703-70389515-c8e6-495f-a741-fab8684ac5de.png)
</br></br>
 
Figure:B-29
</br></br>
The exception is caught by taking the request try catch block and the caught error is written to a txt file. While doing this, a dependency injection is performed. It is sufficient to give the parameter to be used as a parameter to the Invoke method with IHostingEnvironment to determine which path the logs will be on.
</br></br>
 ![image](https://user-images.githubusercontent.com/42031794/177804733-c4c11e6d-0d70-435b-844f-82dd65d38356.png)
</br></br>
Figure:B-30

</br></br>
