import './header.css';
import { Link } from 'react-router-dom';

function Header(){
    return(
        
        <header> 
            <h3>Menu</h3>
            <div>
                <Link to="/home">Home</Link>
                <Link to="/listProduct">Products</Link>
                <Link to="/">Login</Link>
            </div>
        </header>
    );
};

export default Header;