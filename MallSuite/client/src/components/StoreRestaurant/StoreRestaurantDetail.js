import React, { useState, useEffect } from 'react';
import { getStoreRestaurantById, deleteStoreRestaurant } from '../../modules/storeRestaurantManager';
import { Card, CardTitle, CardBody, Container, Button } from 'reactstrap';
import { useParams, useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

const StoreRestaurantDetail = () => {
    const [storeRestaurant, setStoreRestaurant] = useState({});
    const { id } = useParams();
    const navigate = useNavigate();

    const getStoreRestaurant = async () => {
        try {
            const storeRestaurantData = await getStoreRestaurantById(id);
            setStoreRestaurant(storeRestaurantData);
        } catch (error) {
            console.error('Error getting storeRestaurant data', error);
        }
    }

    const handleDelete = async () => {
        const confirmDelete = window.confirm("Are you sure you want to delete this StoreRestaurant?");
        if (confirmDelete) {
            try {
                await deleteStoreRestaurant(id);
                navigate("/");
            } catch (error) {
                console.error('Error deleting storeRestaurant', error);
            }
        }
    }

    useEffect(() => {
        getStoreRestaurant();
    }, []);

    return (
        <Container>
            <Card className="welcome-card">
                <CardBody>
                    <CardTitle tag="h1">Store/Restaurant Details</CardTitle>
                </CardBody>
            </Card>
            <Card>
                <CardBody>
                    <p>Name: {storeRestaurant.name}</p>
                    <p>Type: {storeRestaurant.type}</p>
                    <p>Description: {storeRestaurant.description}</p>
                    <p>Contact: {storeRestaurant.contactInfo}</p>
                    <p>Location: {storeRestaurant.location}</p>
                    <p>Category: {storeRestaurant.category?.name}</p>
                    {storeRestaurant.tags?.map((tag, index) => (
                        <p key={index}>Tag: {tag.name}</p>
                    ))}
                </CardBody>
            </Card>
            <div className="mt-4 d-flex justify-content-center gap-3">
                <Button color="warning" onClick={() => navigate(`/storeRestaurant/edit/${id}`)}>Edit</Button>
                <Button color="danger" onClick={handleDelete}>Delete</Button>
            </div>
        </Container>
    );
}

export default StoreRestaurantDetail;
