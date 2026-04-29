class ConnectionApi {
    static timeSpam = 500;

    static logOut = "Home/Logout";
    static host = 'https://localhost:44373/api/';
        
    url = ConnectionApi.host;

    async redirectToLogOut() {
        setTimeout(() => {
            window.location.href = `${ConnectionApi.host}${ConnectionApi.logOut}`;
        }, ConnectionApi.timeSpam);
    }

    async redirectToAction(endoPoint) {
        setTimeout(() => {
            window.location.href = `${ConnectionApi.host}${endoPoint}`;
        }, ConnectionApi.timeSpam);
    }

    // ================================ INSERT ================================

    async SendPost(endpoint, data) {
        const response = await fetch(`${ConnectionApi.host}${endpoint}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });

        if (!response.ok)
            throw new Error(`HTTP error! status: ${response.status}`);

        return response.status === 204 ? null : await response.json();
    }

    // ================================ SELECT ================================

    async SendGet(endpoint) {
        const response = await fetch(`${ConnectionApi.host}${endpoint}`, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        });

        if (!response.ok)
            throw new Error(`HTTP error! status: ${response.status}`);

        return response.status === 204 ? null : await response.json();
    }

    async SendPostWithFile(endpoint, formData) {
        const response = await fetch(`${ConnectionApi.host}${endpoint}`, {
            method: 'POST',
            body: formData, // deja que el navegador setee el boundary
        });

        if (!response.ok)
            throw new Error(`HTTP error! status: ${response.status}`);

        return response.status === 204 ? null : await response.json();
    }

    // ================================ UPDATE ================================

    async SendPut(endpoint, data) {
        const response = await fetch(`${ConnectionApi.host}${endpoint}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data),
        });

        if (!response.ok)
            throw new Error(`HTTP error! status: ${response.status}`);

        return response.status === 204 ? null : await response.json();
    }

    // ================================ DELETE ================================

    async SendDelete(endpoint, data = null) {
        const options = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        };

        // Si tu API acepta body en DELETE, se envía; si no, déjalo en null.
        if (data !== null) options.body = JSON.stringify(data);

        const response = await fetch(`${ConnectionApi.host}${endpoint}`, options);

        if (!response.ok)
            throw new Error(`HTTP error! status: ${response.status}`);

        return response.status === 204 ? null : await response.json();
    }
};

// Objeto general del sistema para hacer peticiones 
const api = new ConnectionApi();