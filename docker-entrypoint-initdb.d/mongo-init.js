db = db.getSiblingDB('sgs');

db.createCollection('products');

const products = [];
for (let i = 1; i <= 10; i++) {
    products.push({
        id: i.toString(),
        category: `Category${i}`,
        description: `Description for product ${i}`,
        isActive: i % 2 === 0,
        name: `Product${i}`,
        picture: `https://picsum.photos/700/700?random=${i}`,
        price: i * 10.5,
        stock: i * 10,
        discount: {
            status: i % 2 === 0,
            value: i % 2 === 0 ? 10 : 0
        }
    });
}

db.products.insertMany(products);
