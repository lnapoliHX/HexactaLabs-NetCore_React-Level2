# HexactaLabs-.NETCore_React

Hexacta 2020

Bienvenidos a los Hexacta Labs

Agenda:
* _Initial_: Presentación de la aplicación básica, pasos para correrla localmente y planteo de la primer actividad: Backend con .NetCore.
* _Level 1_: Se nivelará presentando una aplicación con las actividades de la etapa inicial completas. Planteo de la segunda actividad: Frontend con ReactJS.
* __Level 2: Se presenta la aplicación con las actividades anteriores completas. Planteo de la tercera actividad: FullStack development.__
* _Final_: Se presenta la aplicación completa. Planteo de la actividad final. 


## [Readme General](https://github.com/lnapoliHX/HexactaLabs-NetCore_React-Initial/blob/master/README.md)

## [Documentación](https://github.com/lnapoliHX/HexactaLabs-NetCore_React-Initial/blob/master/Docs/index.md)


# Nivelación 2
En esta presentación disponemos de una aplicación con las actividades de la nivelación 1 resueltas. La sección Categorías ya se encuentra implementada por lo que se puede comparar la solución propuesta con lo realizado anteriormente.
No es necesario utilizar esta versión de la Stock Web pero sí es recomendable para poder tener una base en común.

En esta ocasión el ejercicio nos pide implementar un Backend y un Frontend para la entidad más importante de la aplicación, __Productos__.

IMPORTANTE: Para esta entidad, el modelo va a tener conexión con ProductType y Provider, por lo tanto lo primero que hay que realizar es descomentar la configuración del mapeo en __ModelProfile.cs__:

```
// CreateMap<Product, ProductDTO>()
//     .ForMember(d => d.ProductTypeId, opt => opt.MapFrom(s => s.ProductType.Id))
//     .ForMember(d => d.ProductTypeDesc, opt => opt.MapFrom(s => s.ProductType.Description))
//     .ReverseMap()
//     .ForMember(s => s.Id, opt => opt.Ignore())
//     .ForMember(s => s.ProductType, opt => opt.Ignore());
```

Luego en __Startup.cs__ hay que descomentar lo siguiente:
```
//services.AddTransient<ProductService>();
```

El sistema debe ser capaz de:
* Crear, editar y eliminar Productos asociados a una categoría existente a través de la sección __Productos__ dentro del sitio.
* Realizar búsquedas de productos.
* La web React debe conectarse con los servicios configurando un store.

## Arquitectura esperada
Se crearon UnitTest para validar los endpoints.
Deberán utilizarlos como guía para que los mismos validen que el desarrollo cumpla con las condiciones, siguiendo la nomeclatura declarada dentro de los tests.
Podrán encontrarlo dentro del proyecto Stock.Test (https://github.com/lnapoliHX/HexactaLabs-NetCore_React-Level2/tree/master/Stock.Test).

Documentación útil:
https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-nunit

## Tips
- Tener encuenta lo utilizado en *initial* sobre inyección de dependencias, automapper y swagger.
    - En su defecto, revisar la [Documentación](https://github.com/lnapoliHX/HexactaLabs-NetCore_React-Initial/blob/master/Docs/index.md) nuevamente.