import React from 'react';
import { BrowserRouter as Router, Switch, Route, Link, NavLink } from 'react-router-dom';
import Registro from '../components/login/Registro';
import Home from '../components/login/Home';
import PerfilGeneral from '../components/perfiles/PerfilGeneral';

const Rutas = () => {
    return (
        <Router>
            <Switch>
                <Route path="/" exact>
                    <Home />
                </Route>
                <Route path="/perfil">
                    <PerfilGeneral />
                </Route>
                <Route path="/registro">
                    <Registro />
                </Route>
            </Switch>
        </Router>
    );
}

export default Rutas;