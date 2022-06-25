import React from "react";
import Spinner from 'react-bootstrap/Spinner';
import cl from './Loader.module.css';

const Loader = () => {
    return (
        <Spinner className={cl.loader} animation="border" />
    );
}

export default Loader;