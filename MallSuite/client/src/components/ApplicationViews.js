import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./auth/Login";
import Register from "./auth/Register";
import Home from "./Home";
import StoreRestaurantDetail from "./StoreRestaurant/StoreRestaurantDetail";
import StoreRestaurantEdit from "./StoreRestaurant/StoreRestaurantEdit";
import AddStoreRestaurant from "./StoreRestaurant/AddStoreRestaurant";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <main>
      <Routes>
        <Route path="/">
          <Route
            index
            element={isLoggedIn ? <Home /> : <Navigate to="/login" />}
          />
          <Route path="login" element={isLoggedIn ? <Navigate to="/" /> : <Login />} />
          <Route path="register" element={isLoggedIn ? <Navigate to="/" /> : <Register />} />
          <Route path="storeRestaurant/add" element={isLoggedIn ? <AddStoreRestaurant /> : <Navigate to="/login" />} />
          <Route path="storeRestaurant/:id" element={isLoggedIn ? <StoreRestaurantDetail /> : <Navigate to="/login" />} />
          <Route path="storeRestaurant/edit/:id" element={isLoggedIn ? <StoreRestaurantEdit /> : <Navigate to="/login" />} />
          <Route path="*" element={isLoggedIn ? <p>Whoops, nothing here...</p> : <Navigate to="/login" />} />
        </Route>
      </Routes>
    </main>
  );
};
