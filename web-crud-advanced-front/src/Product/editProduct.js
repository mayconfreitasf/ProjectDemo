import { useParams, useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';
import './createProduct.css'

function EditProduct() {
    const { id } = useParams();
    const [product, setProduct] = useState({ name: '', description: '', price: 0 });
    const navigate = useNavigate();

    useEffect(() => {
        fetch(`https://localhost:7056/productId/${id}`)
            .then((response) => response.json())
            .then((data) => {
                setProduct(data);
            })
            .catch((error) => console.error('Erro ao buscar o produto:', error));
    }, [id]); 
    const handleSubmit = (e) => {
        e.preventDefault();
        fetch('https://localhost:7056/productEdit', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(product),
        })
        .then((response) => response)
        .then((data) => {
            navigate('/listProduct');
        })
        .catch((error) => console.error('Erro ao atualizar o produto:', error));
    };

    
    const handleBackList = () => {
        navigate('/listProduct');
    };

    return (
        <div className="container-create">
            <h3 className="title-create">Edit Product</h3>
            <form className="product-form" onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Name:</label>
                    <input
                        type="text"
                        
                        value={product.name}
                        onChange={(e) => setProduct({ ...product, name: e.target.value })}
                    />
                </div>

                <div className="form-group">
                    <label>Price:</label>
                    <input
                        type="number"
                        className="form-control"
                        value={product.price}
                        onChange={(e) => setProduct({ ...product, price: parseFloat(e.target.value) })}
                    />
                </div>

                <div className="form-group">
                    <label>Description:</label>
                    <textarea
                        value={product.description}
                        onChange={(e) => setProduct({ ...product, description: e.target.value })}
                    />
                </div>
                <div className="button-container">
                    <button type="submit" className="btn save-btn">Save Changes</button>
                    <button type="button" onClick={() => handleBackList()} className="btn back-btn">
                        Return to List
                    </button>
                </div>
            </form>
      </div>
    );
}

export default EditProduct;