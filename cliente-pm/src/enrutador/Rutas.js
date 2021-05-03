import React from 'react';
import { BrowserRouter as Router, Switch, Route, Link, NavLink } from 'react-router-dom';
import Registro from '../components/Login/Registro';
import Home from '../components/Login/Home';
import PerfilGeneral from '../components/perfiles/PerfilGeneral';
import Navbar from '../components/Navbar';
import { Pases } from '../components/Pases/Pases';

const Rutas = () => {
    return (
        <Router>
            <Switch>
                <Route path="/" exact>
                    <Home />
                </Route>
                <Route path="/perfil">
                    <Navbar />
                    <PerfilGeneral />
                </Route>
                <Route path="/registro">
                    <Registro />
                </Route>
                <Route path="/SolicitudPases">
                    <Navbar />
                    <Pases />
                </Route>
            </Switch>
        </Router>
    );
}

export default Rutas;