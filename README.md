# Laboratorio de Evaluación de Candidatos – Parte 2

Desarrollado por **Juan Mejia**

---

## Descripción del Proyecto

Este repositorio contiene el desarrollo del **Frontend del sistema POS para una pizzería**.

**Importante:**
Antes de ejecutar este proyecto, debes haber configurado y ejecutado correctamente la **Parte 1 (API)**, ya que este frontend depende completamente de ella.

---

## Pasos para Ejecutar el Proyecto

### Clonar el repositorio

```bash
git clone https://github.com/juancholol9/PizzeriaPOSForm.git
```

---

### Abrir la solución

1. Ingresar a la carpeta del proyecto
2. Abrir el archivo:

```
PosPizza.sln
```

Esto abrirá el proyecto en Visual Studio.

---

### Verificar que la API esté en ejecución

Antes de ejecutar el frontend:

* Asegúrate de que la **API (Parte 1)** esté corriendo
* Copia el puerto donde se esté ejecutando, por ejemplo:

```
http://localhost:5099/api
```

---

### Configurar la URL base de la API

1. Ir al archivo:

```
Controllers/AuthService.cs
```

2. Buscar la variable:

```csharp
_baseUrl
```

3. Pegar la URL de la API (incluyendo el puerto correcto)

Ejemplo:

```csharp
private string _baseUrl = "http://localhost:5099/api";
```

---

### Ejecutar el Proyecto

Una vez configurado correctamente:

* Ejecutar el proyecto desde Visual Studio
* Iniciar sesión con el **usuario colaborador** creado en la Parte 1

---

## Tecnologías Utilizadas

* .NET
* Consumo de API REST
* Autenticación basada en tokens
* Arquitectura por capas

---

## Notas Importantes

* La API debe estar ejecutándose antes de iniciar el frontend
* Verifica que el puerto configurado coincida exactamente con el puerto donde corre la API
* Asegúrate de haber creado un colaborador en la base de datos para poder iniciar sesión

---

Proyecto desarrollado como parte de un laboratorio técnico para evaluación de candidatos.
