import {useState, useEffect} from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './product.css'

function ListProduct(){
    const [products, setProducts] = useState([]);
    const navigate = useNavigate();
    useEffect(() => {   
        const apiUrl = 'https://localhost:7056/api/products';
        fetch(apiUrl, {
            method: 'GET',
            headers: {
            'Content-Type': 'application/json'  
            },
        })
        .then((response) => { return response.json(); })
        .then((data) => { 
            const items = Object.values(data).filter(item => typeof item === 'object' && !Array.isArray(item));
            setProducts(items);
        })
        .catch((error) => { console.error('Erro:', error); });
    }, []);

    useEffect(() => {
        
      }, [products]);

    const handleDelete = (id) => {
        const apiUrl = 'https://localhost:7056/productDelete/'+id;
        fetch(apiUrl, {
            method: 'DELETE',
            headers: {
            'Content-Type': 'application/json'  
            },
        })
        .then((response) => { return response; })
        .then((data) => { 
            console.log('Response:', data); 
            setProducts((prevProducts) => prevProducts.filter(product => product.id !== id));
        })
        .catch((error) => { console.error('Erro:', error); });
    };

    const handleEdit = (id) => {

        navigate(`/editProduct/${id}`);
    };

    return(
        <div className="container">
        <h3 className="list-title">Product List</h3>
        <div className="form-group">
            <Link to="/createProduct">Create a new product</Link>
        </div>
        <ul className="product-list">
            {products.map((product) => (
            <li  key={product.id}>
                <h3>Product: {product.name}</h3>
                <p>Description: {product.description}</p>
                <div className="price-button-container">
                <p>Price: ${product.price}</p>
                <div className="buttons-container">
                    <button type="button" onClick={() => handleEdit(product.id)}>Edit</button>
                    <button type="button" onClick={() => handleDelete(product.id)}>Delete</button>
                </div>
                </div>
            </li>

            ))}
        </ul>
        </div>
    );
}
export default ListProduct;