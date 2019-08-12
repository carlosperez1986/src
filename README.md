# Modular Proyect .net core 2.2 - SOLID, Arquitecture DDD #

 Modular Proyect .net core 2.2 - SOLID, Arquitecture DDD 

Proyecto creado para tener una plantilla en .NET CORE con una implementación modular. <br/>

En la carpeta Modules se encuentran los proyectos. <br/>
Para que los módulos sean inyectados debemos compilarlos y colocar la carpeta bin, views dentro de la carpeta Modules en el proyecto Webapplication. <br/>

<p>En el Startup de este proyecto(Webapplication), se encuenta la lógica para inyectarlos.
<br>
Introducida la capa de datos, agregado irepository<model> para mapear las identidades utilizadas.
Agregado AddCustomizedIdentity, para utilizar la iautentication de Core .NET.
Agregado ViewComponent Modular: 'return View(ViewComponentExtensions.GetViewPath(this), Model)'
</p>
--
Estoy tratando de resolver como llevar a cabo una arquitectura DDD.<br/>
--
No olvidar :<br/>
<ul><li>
     dotnet ef migrations add 'InitialMigration'</li>
     <li>dotnet ef database update</li>
</ul>  
<br/>
He conectado con docker y kinematic a una database en sql server, para gestionar la base de datos recomiendo la herramienta <strong>Azure Data Studio</strong>



Fucking jon snow !!!!!!!!
 
<hr>

<h3> Modular Proyect .net core 2.2 - SOLID, DDD Architecture </h3>
Project created to have a template in .NET CORE with a modular implementation. <br>

Projects are located in the Modules folder. <br>
For the modules to be injected, we must compile them and place the bin folder, views inside the Modules folder in the Webapplication project. <br>
In the Startup of this project (Webapplication), the logic to inject them is found.
<br>
Introduced the data layer, added irepository <model> to map the identities used.
Added AddCustomizedIdentity, to use Core .NET authentication.

Fucking Jon Snow !!!!!!!!
