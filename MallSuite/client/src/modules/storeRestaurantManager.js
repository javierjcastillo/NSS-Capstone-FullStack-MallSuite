import { getToken } from "./authManager";

const baseUrl = '/api/StoreRestaurant';

export const getAllStoreRestaurants = () => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: 'GET',
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
};

export const getStoreRestaurantById = (id) => {
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

export const addStoreRestaurant = (storeRestaurant) => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(storeRestaurant)
        }).then(res => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error('Error: ' + res.status);
            }
        });
    });
}

export const updateStoreRestaurant = (storeRestaurant) => {
    return getToken().then((token) => {
        console.log(storeRestaurant);
        return fetch(`${baseUrl}/${storeRestaurant.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(storeRestaurant)
        }).then(res => {
            if (res.ok) {
                if (res.status === 204) {
                    return;
                } else {
                    return res.json();
                }
            } else {
                throw new Error('Error: ' + res.status);
            }
        });
    });
}

export const deleteStoreRestaurant = (id) => {
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

export const getStoreRestaurantsByCategoryId = (categoryId) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/ByCategory/${categoryId}`, {
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

export const getStoreRestaurantsByTagId = (tagId) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/ByTag/${tagId}`, {
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

export const addCategoryToStoreRestaurant = (storeRestaurantId, categoryId) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/AddCategory/${storeRestaurantId}/${categoryId}`, {
            method: "POST",
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

export const updateCategoryInStoreRestaurant = (storeRestaurantId, categoryId) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/UpdateCategory/${storeRestaurantId}/${categoryId}`, {
            method: "PUT",
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

export const addTagToStoreRestaurant = (storeRestaurantId, tagId) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/AddTag/${storeRestaurantId}/${tagId}`, {
            method: "POST",
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

export const removeTagFromStoreRestaurant = (storeRestaurantId, tagId) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/RemoveTag/${storeRestaurantId}/${tagId}`, {
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
