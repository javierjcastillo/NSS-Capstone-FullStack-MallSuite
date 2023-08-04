import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Card, CardBody, Container, Form, FormGroup, Label, Input, Button, CardTitle } from 'reactstrap';
import { addStoreRestaurant } from '../../modules/storeRestaurantManager';
import { getAllCategories } from '../../modules/categoryManager';
import { getAllTags } from '../../modules/tagManager';
import { getUserByFirebaseId } from '../../modules/userManager';
import firebase from 'firebase/app';

const AddStoreRestaurant = () => {
    const navigate = useNavigate();
    const [currentUser, setCurrentUser] = useState(null); // Logged in user
    const [categories, setCategories] = useState([]);
    const [tags, setTags] = useState([]);
    const [storeRestaurant, setStoreRestaurant] = useState({
        type: '',
        name: '',
        description: '',
        categoryId: '',
        location: '',
        contactInfo: '',
        userId: '',  // This should be set based on your logged in user
        tags: [],
    });

    const getTagsData = async () => {
        try {
            const tagsData = await getAllTags();
            setTags(tagsData);
        } catch (error) {
            console.error('Error getting tags data', error);
        }
    }

    // Get the current user's ID from firebase
    useEffect(() => {
        const firebaseUserId = firebase.auth().currentUser.uid; // The UID of the current user
        getUserByFirebaseId(firebaseUserId).then(setCurrentUser);
    }, []);

    useEffect(() => {
        getAllCategories()
            .then(categories => setCategories(categories))
            .catch(error => console.log(error));
    }, []);

    useEffect(() => {
        getTagsData();
    }, []);


    const handleChange = (event) => {
        const newStoreRestaurant = { ...storeRestaurant };
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.id;

        if (target.type === 'checkbox') {
            let newTags;
            if (value) { // checkbox is checked
                newTags = [...storeRestaurant.tags, { id: Number(name), name: 'tag' }]; // Add tag to array
            } else { // checkbox is unchecked
                newTags = storeRestaurant.tags.filter(tag => tag.id !== Number(name)); // Remove tag from array
            }
            newStoreRestaurant['tags'] = newTags;
        } else {
            newStoreRestaurant[name] = value; // update the value on the storeRestaurant
        }
        setStoreRestaurant(newStoreRestaurant); // update the state with the new storeRestaurant
    };


    const handleSave = (e) => {
        e.preventDefault();
        // Check if a valid category has been selected
        if (storeRestaurant.categoryId === '' || storeRestaurant.categoryId === '0') {
            console.error('Error: Please select a valid category');
            return;
        }
        // Check if a user is currently logged in
        if (currentUser) {
            const newStoreRestaurant = { ...storeRestaurant };
            newStoreRestaurant.userId = currentUser.id; // Set the userId on the new storeRestaurant
            addStoreRestaurant(newStoreRestaurant)
                .then(() => {
                    navigate("/");
                })
                .catch((err) => console.error('Error adding store restaurant:', err));
        } else {
            console.error('Error: No user is currently logged in');
        }
    };

    return (
        <Container>
            <Card className="welcome-card">
                <CardBody>
                    <CardTitle tag="h1">Add a New Store/Restaurant</CardTitle>
                </CardBody>
            </Card>
            <Card className="border-0">
                <CardBody>
                    <Form>
                        <FormGroup>
                            <Label for="name">Name</Label>
                            <Input type="text" id="name" value={storeRestaurant.name || ''} onChange={handleChange} />
                        </FormGroup>
                        <FormGroup>
                            <Label for="type">Type</Label>
                            <Input type="select" id="type" value={storeRestaurant.type || ''} onChange={handleChange}>
                                <option value="0">Select a type</option>
                                <option value="Restaurant">Restaurant</option>
                                <option value="Store">Store</option>
                            </Input>
                        </FormGroup>
                        <FormGroup>
                            <Label for="description">Description</Label>
                            <Input type="text" id="description" value={storeRestaurant.description || ''} onChange={handleChange} />
                        </FormGroup>
                        <FormGroup>
                            <Label for="contactInfo">Contact Info</Label>
                            <Input type="text" id="contactInfo" value={storeRestaurant.contactInfo || ''} onChange={handleChange} />
                        </FormGroup>
                        <FormGroup>
                            <Label for="location">Location</Label>
                            <Input type="text" id="location" value={storeRestaurant.location || ''} onChange={handleChange} />
                        </FormGroup>
                        <FormGroup>
                            <Label for="categoryId">Category</Label>
                            <Input type="select" id="categoryId" value={storeRestaurant.categoryId || ''} onChange={handleChange}>
                                <option value="0">Select a category</option>
                                {categories.map((category) => (
                                    <option key={category.id} value={category.id}>
                                        {category.name}
                                    </option>
                                ))}
                            </Input>
                        </FormGroup>
                        <FormGroup>
                            <Label for="tags">Tags</Label>
                            {tags.map((tag) => (
                                <FormGroup check key={tag.id}>
                                    <Label check>
                                        <Input type="checkbox" id={String(tag.id)} checked={storeRestaurant.tags && storeRestaurant.tags.some(t => t.id === tag.id)} onChange={handleChange} />
                                        {tag.name}
                                    </Label>
                                </FormGroup>
                            ))}
                        </FormGroup>
                        <Button color="primary" onClick={handleSave}>Submit</Button>
                    </Form>
                </CardBody>
            </Card>
        </Container>
    );
};

export default AddStoreRestaurant;
