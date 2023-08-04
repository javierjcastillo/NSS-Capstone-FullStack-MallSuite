import React, { useState, useEffect } from 'react';
import { getAllStoreRestaurants, getStoreRestaurantsByCategoryId, getStoreRestaurantsByTagId } from '../modules/storeRestaurantManager';
import { getAllCategories } from '../modules/categoryManager';
import { getAllTags } from '../modules/tagManager';
import { Link } from 'react-router-dom';
import { Card, CardTitle, CardBody, Input, Container, Row, Col } from 'reactstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Home.css';

const Home = () => {
  const [storeRestaurants, setStoreRestaurants] = useState([]);
  const [categories, setCategories] = useState([]);
  const [tags, setTags] = useState([]);
  const [selectedCategoryId, setSelectedCategoryId] = useState("");
  const [selectedTagId, setSelectedTagId] = useState("");

  const getInitialData = async () => {
    try {
      const storeRestaurantData = await getAllStoreRestaurants();
      const categoryData = await getAllCategories();
      const tagData = await getAllTags();
      setStoreRestaurants(storeRestaurantData);
      setCategories(categoryData);
      setTags(tagData);
    } catch (error) {
      console.error('Error getting initial data', error);
    }
  }

  useEffect(() => {
    getInitialData();
  }, []);

  const handleCategoryChange = async (event) => {
    setSelectedCategoryId(event.target.value);
    if (event.target.value === "") {
      getInitialData();
    } else {
      const filteredData = await getStoreRestaurantsByCategoryId(event.target.value);
      setStoreRestaurants(filteredData);
    }
  }

  const handleTagChange = async (event) => {
    setSelectedTagId(event.target.value);
    if (event.target.value === "") {
      getInitialData();
    } else {
      const filteredData = await getStoreRestaurantsByTagId(event.target.value);
      setStoreRestaurants(filteredData);
    }
  }

  return (
    <Container>
      <Card className="welcome-card">
        <CardBody>
          <CardTitle tag="h1">Welcome To MallSuite</CardTitle>
        </CardBody>
      </Card>

      <Row>
        <Col md={6}>
          <Input className="filter-select" type="select" value={selectedCategoryId} onChange={handleCategoryChange}>
            <option value="">-- Filter by Category --</option>
            {categories.map(category => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </Input>
        </Col>

        <Col md={6}>
          <Input className="filter-select" type="select" value={selectedTagId} onChange={handleTagChange}>
            <option value="">-- Filter by Tag --</option>
            {tags.map(tag => (
              <option key={tag.id} value={tag.id}>
                {tag.name}
              </option>
            ))}
          </Input>
        </Col>
      </Row>

      <Row>
        <Col>
          <h2 className="store-list-header">List of Stores & Restaurants</h2>
        </Col>
      </Row>

      <Row>
        {storeRestaurants.map(storeRestaurant => (
          <Col sm={6} md={4} lg={3} key={storeRestaurant.id}>
            <Card className="mb-4">
              <CardBody>
                <Link to={`/storeRestaurant/${storeRestaurant.id}`}>
                  <h5>{storeRestaurant.name}</h5>
                </Link>
                <p className="mb-0">Contact: {storeRestaurant.contactInfo}</p>
                <p className="mb-0">Location: {storeRestaurant.location}</p>
                <p className="mb-0">Category: {storeRestaurant.category?.name}</p>
              </CardBody>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
}

export default Home;
