import firebase from "firebase/app";
import "firebase/auth";

const baseUrl = "/api/user";

export const getUserDetails = (firebaseUUID) => {
  return getToken().then(token => {
    return fetch(`${baseUrl}/${firebaseUUID}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(res => res.json())
  })
}

const _doesUserExist = (firebaseUserId) => {
  return getToken().then((token) =>
    fetch(`${baseUrl}/DoesUserExist/${firebaseUserId}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json()));
};

const _saveUser = (user) => {
  return getToken().then((token) =>
    fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(user)
    }).then(resp => resp.json()));
};

export const getToken = () => firebase.auth().currentUser.getIdToken();


export const login = (email, pw) => {
  return firebase.auth().signInWithEmailAndPassword(email, pw)
    .then((signInResponse) => _doesUserExist(signInResponse.user.uid))
    .then((doesUserExist) => {
      if (!doesUserExist) {

        // If we couldn't find the user in our app's database, or the user is deactivated, we should logout of firebase
        logout();

        throw new Error("Something's wrong. The user exists in firebase, but not in the application database. (User may be deactivated)");
      }
    }).catch(err => {
      console.error(err);
      throw err;
    });
};


export const logout = () => {
  firebase.auth().signOut()
};


export const register = (user, password) => {
  return firebase.auth().createUserWithEmailAndPassword(user.email, password)
    .then((createResponse) => _saveUser({
      ...user,
      firebaseUserId: createResponse.user.uid
    }));
};


export const onLoginStatusChange = (onLoginStatusChangeHandler) => {
  firebase.auth().onAuthStateChanged((user) => {
    onLoginStatusChangeHandler(!!user);
  });
};