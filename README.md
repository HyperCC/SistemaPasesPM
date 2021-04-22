# SistemaPasesPM
Proyecto Capstone - Sistema de pases en PMEJ


## Uso de Branch

Durante el desarrollo se registraran todos los cambios principalmente en el branch **dev**. 
Una vez cumplidos puntos del cronograma o el kanban se registraran en la branch principal **main**.

Se pueden utilizar los Branch que estimen necesarios fuera del branch **dev** y **main** pero se debe notificar al equipo respecto al uso de estas y en que capa se trabaja, 
de no hacerlo se corre el riesgo de sobreescribir el codigo que esta realizando otro integrante del equipo y se perderia mucho tiempo.


## Creacion de commits
Estandar general para crear commit durante todo el proyecto.


### Formato para hacer commits 
Los commit creados para el proyecto deben guiarse por la siguiente sintaxis:

    tipo(capa-contexto): titular
    
En caso de que se requiera extender la explicacion del cambio realizado la sintaxis sera:

    tipo(capa-contexto): titular
    descripcion completa ...
    
Algunos ejemplos de commit segun las normas:

    feat(w-login): add habilitado ingreso por correo de usuario 
    
    fix(d-Usuario): udpate cambiado tipo de atributo de Nombre
    
    chore(p-ORM): delete Entity Framework ORM reemplazada por Dapper


### Tipos de commit 
la primera palabra en ingles define tipo del commit que se hace entre los cuales se encuentran los siguientes:

    * feat: se añade una nueva característica o funcionalidad.
    * docs: cambios de documentación (comentarios o documentacion complementaria).
    * chore: actualización de dependencias o librerias en una capa del proyecto.
    * fix: arreglos de todo tipo.
    * perf: un cambio que mejora el rendimiento (por ejemplo el uso de threads).
    * refactor: un cambio que no corrige un error ni agrega una característica.
    * style: cambio unicamente visual del codigo (se añaden espacios en blanco, comillas faltantes, etc..).
    * test: se añaden pruebas de software (especificar el tipo de test) o se actualizan pruebas existentes.
    
    
### Capas y Contexto del commit 
El texto entre los parentesis tiene 2 cosas importantes, la **capa** o parte del proyecto modificada y la contexto.
la primera letra identifica la capa y existen los siguientes tipo:

    w: WebApi
    a: Aplicacion
    c: cliente-pm
    d: Dominio
    p: Persistencia
    
Esta letra identificativa puede omitirse unicamente si se realizan cambios que afecten a todo el proyecto o a ninguno en especifico (por ejemplo este archivo README.md).
    
Las siguientes palabras seguida de un guión despues del tipo de **capa** es el contexto. 
El contexto puede contener mas de una palabra para describir el area afectada (el area o contexto dentro de la capa) 
no siendo obligacion utilizar mayusculas a menos que se referencie una clase o metodo en especifico.
Se recomienda no superar los 10 caracteres.
    
    
### Titular o descripcion minima del commit 
El tituloar debe comenzar con la primera palabra en ingles que identifique que **acction** se realizo en el commit. Las acciones de ejemplo:

    add
    delete
    update
    
Pueden haber mas acciones en ingles de ser necesarias.
    
El resto del contenido debe contener una descripcion minima del cambio realizado escrito en español con los minimos caracteres posibles.
Se recomienda no superar los 40 caracteres de titular, el commit completo no deberia ser mas de una linea.


### Descripcion completa del commit 
Este punto es 100% opcional.
En caso de que el titular no alcance a describir el cambio realizado por el commit (dado que su longitud en caracteres debe ser muy limitada)
esta la opcion de describir con lujo de detalles todo aquello que sea relevante para comprender los cambios realizados.
La descripcion del commit no tiene limite de caracteres y es completamente en español.
    
    
    
    
    
    
    
    
    
    
 
