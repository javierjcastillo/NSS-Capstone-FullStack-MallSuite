import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Card, CardBody, Container, Form, FormGroup, Label, Input, Button, CardTitle } from 'reactstrap';
import { getStoreRestaurantById, updateStoreRestaurant } from '../../modules/storeRestaurantManager';
import { getAllCategories } from '../../modules/categoryManager';
import { getAllTags } from '../../modules/tagManager';

const StoreRestaurantEdit = () => {
    const [storeRestaurant, setStoreRestaurant] = useState({});
    const [categories, setCategories] = useState([]);
    const [tags, setTags] = useState([]);
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

    const getCategoriesData = async () => {
        try {
            const categoriesData = await getAllCategories();
            setCategories(categoriesData);
        } catch (error) {
            console.error('Error getting categories data', error);
        }
    }

    const getTagsData = async () => {
        try {
            const tagsData = await getAllTags();
            setTags(tagsData);
        } catch (error) {
            console.error('Error getting tags data', error);
        }
    }


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
            newStoreRestaurant[name] = value;
        }
        setStoreRestaurant(newStoreRestaurant);
    };

    const handleSave = async () => {
        const { user, ...storeRestaurantToUpdate } = storeRestaurant;

        try {
            await updateStoreRestaurant(storeRestaurantToUpdate);
            navigate(`/storeRestaurant/${storeRestaurant.id}`); // route to the details page of this StoreRestaurant
        } catch (error) {
            console.error('Error updating storeRestaurant', error);
        }
    }


    useEffect(() => {
        getStoreRestaurant();
        getCategoriesData();
        getTagsData();
    }, []);

    return (
        <Container>
            <Card className="welcome-card">
                <CardBody>
                    <CardTitle tag="h1">Store/Restaurant Edit</CardTitle>
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
                        <Button color="primary" onClick={handleSave}>Save</Button>
                    </Form>
                </CardBody>
            </Card>
        </Container>
    );
}

export default StoreRestaurantEdit;
