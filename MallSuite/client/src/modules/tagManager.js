import { getToken } from "./authManager";

const baseUrl = "/api/Tag"

export const getAllTags = () => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error('Error in the API request');
            }
        });
    });
}

export const getTagById = (id) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error('Error: ' + res.status);
            }
        });
    });
}

export const addTag = (tag) => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(tag)
        }).then(res => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error('Error: ' + res.status);
            }
        });
    });
}

export const editTag = (tag) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/${tag.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(tag)
        }).then(res => {
            if (res.ok) {
                return;
            } else {
                throw new Error('Error: ' + res.status);
            }
        });
    });
}

export const deleteTag = (id) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => {
            if (res.ok) {
                return;
            } else {
                throw new Error('Error: ' + res.status);
            }
        });
    });
}
