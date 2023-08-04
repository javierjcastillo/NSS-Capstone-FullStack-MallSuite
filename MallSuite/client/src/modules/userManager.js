import { getToken } from "./authManager";

const baseUrl = "/api/User"

export const getUserByFirebaseId = (firebaseUserId) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/${firebaseUserId}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(resp => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(`An error occurred while retrieving user by Firebase user Id: "${firebaseUserId}"`)
            }
        });
    });
}

export const doesUserExist = (firebaseUserId) => {
    return fetch(`${baseUrl}/DoesUserExist/${firebaseUserId}`, {
        method: "GET"
    }).then(resp => {
        if (resp.ok) {
            return resp.json()
        }
        else {
            throw new Error(`An error occurred while checking if user exists. Firebase user Id: "${firebaseUserId}"`)
        }
    })
}

export const getUserById = (id) => {
    return fetch(`${baseUrl}/details/${id}`, {
        method: "GET"
    }).then(resp => {
        if (resp.ok) {
            return resp.json()
        }
        else {
            throw new Error(`An error occurred while retrieving user by id: "${id}"`)
        }
    })
}

export const postUser = (user) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
    }).then(resp => {
        if (resp.ok) {
            return resp.json()
        }
        else {
            throw new Error("An error occurred while posting this user. Is it formatted correctly?")
        }
    })
}

export const deleteUser = (userId) => {
    return fetch(`${baseUrl}/${userId}`, {
        method: "DELETE"
    }).then(resp => {
        if (resp.ok) {
            return
        }
        else {
            throw new Error(`An error occurred while deleting user with id ${userId} - does it exist?`)
        }
    })
}
