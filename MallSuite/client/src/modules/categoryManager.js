import { getToken } from "./authManager";

const categoryUrl = "/api/Category"

export const getAllCategories = () => {
    return getToken().then((token) => {
        return fetch(categoryUrl, {
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

export const getCategoryById = (id) => {
    return getToken().then((token) => {
        return fetch(`${categoryUrl}/${id}`, {
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

export const addCategory = (category) => {
    return getToken().then((token) => {
        return fetch(categoryUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(category)
        }).then(res => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error('Error: ' + res.status);
            }
        });
    });
}

export const deleteCategory = (id) => {
    return getToken().then((token) => {
        return fetch(`${categoryUrl}/${id}`, {
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

export const editCategory = (category) => {
    return getToken().then((token) => {
        return fetch(`${categoryUrl}/${category.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(category)
        }).then(res => {
            if (res.ok) {
                return;
            } else {
                throw new Error('Error: ' + res.status);
            }
        });
    });
}
