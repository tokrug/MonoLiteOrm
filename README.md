MonoLiteOrm
===========

Simple ORM to be used in Unity3D with Sqlite.

Why bother creating yet another ORM? Basically because there is none that can be easily used in Unity3D. Most of them are dependent on .NET 3.5 and even if they are not they still have some issues (like storing complex objects as serialized text rather than in another table).

API is based on JPA/Hibernate (dunno if nHibernate looks the same). Of course it doesn't provide every single feature known from Hibernate. There will be no lazy loading and the sort. 

The aim is to create API for CRUD operations for whole object graphs but only dependent on Mono libraries (.NET 2.0).
