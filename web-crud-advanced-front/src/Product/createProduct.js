import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './createProduct.css'

function CreateProduct(){
    const [name, setName] = useState('');
    const [price, setPrice] = useState('');
    const [description, setDescription] = useState('');
    const navigate = useNavigate();
  
    const handleSave = () => {

        const loggedUser = JSON.parse(localStorage.getItem('loggedUser'));

        const Product = {
            Name: name,
            Price: price,
            Description: description,
            UserId: loggedUser.id
        };
        console.log(JSON.stringify(Product)  );
        setName('');
        setPrice('');
        setDescription('');

        const apiUrlP = 'https://localhost:7056/productCreate';
        fetch(apiUrlP, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json'  
            },
            body: JSON.stringify(Product)  
        })
        .then((response) => { console.log(response);return response.json(); })
        .then((data) => { console.log('Response:', data); })
        .catch((error) => {
            console.error('Error:', error.message);
            if (error.response) {
                console.error('Server Response:', error.response);
            }
        });
    };
  
    const handleClear = () => {
      setName('');
      setPrice('');
      setDescription('');
    };

    const handleBackList = () => {
        navigate('/listProduct');
    };
  
    return (
      <div className="container-create">
        <h3 className="title-create">New Product</h3>
        <form className="product-form">
          <div className="form-group">
            <label htmlFor="name">Name</label>
            <input
              type="text"
              id="name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Enter product name"
            />
          </div>
  
          <div className="form-group">
            <label htmlFor="price">Price</label>
            <input
              type="number"
              id="price"
              value={price}
              onChange={(e) => setPrice(parseInt(e.target.value))}
              placeholder="Enter product price"
            />
          </div>
  
          <div className="form-group">
            <label htmlFor="description">Description</label>
            <textarea
              id="description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Enter product description"
            ></textarea>
          </div>
  
          <div className="button-container">
            <button type="button" onClick={handleSave} className="btn save-btn">
              Save
            </button>
            <button type="button" onClick={handleClear} className="btn clear-btn">
              Clear
            </button>
            <button type="button" onClick={handleBackList} className="btn back-btn">
              Return to List
            </button>
          </div>
        </form>

        
      </div>
    );
  }
export default CreateProduct;