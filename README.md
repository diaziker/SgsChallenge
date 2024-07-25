## Goal of the Exercise

The goal of this exercise is to build an HTTP REST API that allows users to interact with a product database. The API should handle GET requests to retrieve products and provide several key features for usability and flexibility. Below is a breakdown of what needs to be achieved:

### Key Considerations

1. **ID vs _id in MongoDB:**
- **Clarification:** In MongoDB, the `_id` field is used as the primary key for each document and is automatically generated if not provided. This `_id` is different from the `id` field that might be included in sample data. In the provided example JSON, the `id` field represents a unique identifier, but in MongoDB, the actual unique identifier for each document is stored in the `_id` field.

### **1. Build a RESTful API**
- **Endpoint:** Create endpoints to serve GET requests for retrieving products.
- **Paths:**
  - **Retrieve All Products:** `/api/products` - Retrieve a list of all products.
  - **Retrieve Single Product:** `/api/products/{id}` - Retrieve a single product by its unique ID.
  - **Filtered Retrieval:** `/api/products/filter` - Retrieve products based on specified filter criteria.

### **2. Connect to MongoDB**
- **Database:** Use MongoDB to store and manage product data.
- **Configuration:** Ensure the API connects to MongoDB and performs necessary data operations.

### **3. Implement Filtering**
- **Functionality:** Allow filtering of products based on various attributes:
  - **Category:** Filter by product category (e.g., electronics, clothing).
  - **IsActive:** Filter by active or inactive status.
  - **MinPrice:** Filter by minimum price of the product.
  - **MaxPrice:** Filter by maximum price of the product.
  - **Stock:** Filter by stock quantity.
  - **HasDiscount:** Filter by products with a discount.
- **Implementation:** Pass filtering parameters in the query string of the GET request to `/api/products/filter`.

- **Parameters:**
  - **category**: The category of the product (e.g., electronics, clothing).
  - **minPrice**: Minimum price of the product.
  - **maxPrice**: Maximum price of the product.
  - **isActive**: Filter by active status (true or false).
  - **stock**: Filter by stock quantity (e.g., greater than 10).
  - **hasDiscount**: Filter by products with a discount (true or false).
  - **pageNumber**: Page number for pagination (default is 1).
  - **pageSize**: Number of products per page (default is 10).
  - **sortBy**: Field to sort by (default is Name; can be Price, Category, etc.).
  - **ascending**: Sort direction (true for ascending, false for descending).

### **4. Implement Sorting**
- **Functionality:** Enable sorting of products by attributes such as:
  - **Price:** Sort by price in ascending or descending order.
  - **Name:** Sort alphabetically by product name.
- **Implementation:** Specify sorting options in the query string of the GET request to `/api/products` or `/api/products/filter`.

### **5. Implement Pagination**
- **Functionality:** Support pagination to handle large product sets. Allow clients to:
  - **Page Number:** Specify which page of results to retrieve.
  - **Page Size:** Define the number of products per page.
- **Implementation:** Include pagination parameters in the query string of the GET request to `/api/products` or `/api/products/filter`.

### **6. Example GET Requests**

- **Retrieve All Products:**
  ```http
  GET http://localhost:5000/api/products?pageNumber=1&pageSize=10&ascending=true

- **Retrieve Product by ID:**
  ```http
  GET http://localhost:5000/api/products/1

- **Retrieve Filtered Products:**
  ```http
  GET http://localhost:5000/api/products/filter?category=electronics&minPrice=50&maxPrice=200&isActive=true&stock=10&hasDiscount=true&pageNumber=1&pageSize=10&sortBy=price&ascending=true

### 7. Build and Start the Services

Run the following command to build the Docker images and start the containers:

```sh
docker-compose up --build