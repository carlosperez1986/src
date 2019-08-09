# src
Modular proyect .net core 2.2
Proyecto creado para tener una plantilla en .NET CORE con una implementación modular.
En la carpeta Modules se encuentran los proyectos. 
Para que los módulos sean inyectados debemos compilarlos y colocar la carpeta bin, views dentro de la carpeta Modules de webapplication.
En el Startup de este proyecto(Webapplication), se encuenta la lógica para inyectarlos.
