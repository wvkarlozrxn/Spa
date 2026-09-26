## 🛠️ Guía Rápida de Comandos Git y GitHub

### 📥 1. Clonar y Sincronizar el Repositorio
Comandos para descargar el proyecto y actualizar tu entorno local con los cambios del servidor.

```bash
# Clonar un repositorio remoto por primera vez
git clone <URL>

# Consultar cambios en el servidor sin aplicarlos en tus archivos
git fetch

# Traer y fusionar los últimos cambios del servidor a tu rama actual
git pull
```

---

### 💾 2. Guardar y Publicar Cambios
Flujo de trabajo para rastrear, confirmar y subir modificaciones al servidor.

```bash
# Preparar todos los archivos modificados y nuevos para el commit
git add *

# Confirmar cambios rastreados en un solo paso añadiendo un mensaje
git commit -am "Descripción de los cambios"

# Subir los commits locales al repositorio remoto
git push origin dev
```

---

### 🌿 3. Gestión y Envío de Ramas
Creación, navegación y publicación de ramas de trabajo secundarias.

```bash
# Crear una nueva rama local llamada dev
git branch dev

# Cambiar de rama actual a dev
git checkout dev

# Subir la rama dev1 al servidor y vincularla para futuros push/pull
git push -u origin dev

# Enviar los commits de la rama local actual directamente a main en GitHub
git push origin dev
```

---

### 🔄 4. Reorganización y Descarte de Cambios
Restaura el estado de tus archivos o actualiza tu rama respecto a la principal.

```bash
# Descartar todos los cambios locales no guardados en el directorio actual
git checkout -- .

# Cambiar a la rama principal
git checkout main

# Reorganizar el historial aplicando los cambios de main sobre la rama actual
git rebase main

# En caso de error o bloqueo durante el rebase, eliminar la carpeta de estado:
rm -rf .git/rebase-merge
```

---

### 🔑 5. Gestión de Credenciales en Windows
Si necesitas borrar o restablecer la sesión guardada de GitHub en tu equipo:

```text
Ruta en Windows:
Control Panel\User Accounts\Credential Manager

Instrucciones:
1. Ir a Credenciales de Windows (Windows Credentials).
2. Buscar las entradas asociadas a 'git:https://github.com'.
3. Seleccionar la cuenta y hacer clic en 'Quitar' (Remove).
```
