import { BrowserRouter, Route, Routes} from 'react-router-dom'
import { useState, useEffect } from 'react';
import Login from './Login/login'
import CreateUser from './User/createUser'
import Home from './Home/home'
import CreateProdut from './Product/createProduct'
import ListProduct from './Product/listProduct'
import EditProduct from './Product/editProduct'
import Header from './components/Header/header'

function RoutesApp(){
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [showHeader, setShowHeader] = useState(true);

    useEffect(() => {
     

    const currentPath = window.location.pathname;
    console.log(currentPath);
    setShowHeader(currentPath !== '/' && currentPath !== '/createUser' );
    }, []);
    

    return(
        <BrowserRouter>
        {showHeader && <Header />}
        <Routes>
            <Route path="/" element={ <Login/>}/>
            <Route path="/createUser" element={ <CreateUser/>}/>
            <Route path="/home" element={ <Home/>}/>
            <Route path="/createProduct" element={ <CreateProdut/>}/>
            <Route path="/listProduct" element={ <ListProduct/>}/>
            <Route path="/editProduct/:id" element={<EditProduct/>} />
        </Routes>
        
        </BrowserRouter>
    )
}

export default RoutesApp