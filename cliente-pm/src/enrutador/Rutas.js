import React from 'react';
import { BrowserRouter as Router, Switch, Route, Link, NavLink } from 'react-router-dom';
import Registro from '../components/Login/Registro';
import Home from '../components/Login/Home';
import PerfilGeneral from '../components/perfiles/PerfilGeneral';
import Navbar from '../components/Navbar';
import { Pases } from '../components/Pases/Pases';
import { Visita } from '../components/Pases/Visita';
import RecuperarClave from '../components/Login/RecuperarClave';
import CambiarClave from '../components/Login/CambiarClave';
import AgregarPersona from '../components/Pases/AgregarPersona';
import { Contratista } from '../components/Pases/Contratista';
import { DocumentosEmpresa } from '../components/Pases/DocumentosEmpresa';
import { AgregarPersonaContratista } from '../components/Pases/AgregarPersonaContratista';

const Rutas = () => {
    return (
        <Router>
            <Switch>
                <Route path="/SolicitudVisita/AgregarPersona">
                    <Navbar />
                    <AgregarPersona />
                </Route>
                <Route path="/SolicitudContratista/AgregarPersona">
                    <Navbar />
                    <AgregarPersonaContratista />
                </Route>
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
                <Route path="/SolicitudVisita">
                    <Navbar />
                    <Visita />
                </Route>
                <Route path="/RecuperarContraseña">
                    <RecuperarClave />
                </Route>
                <Route path="/CambiarContraseña">
                    <CambiarClave />
                </Route>
                
                <Route path="/SolicitudContratista">
                    <Navbar />
                    <Contratista />
                </Route>
                <Route path="/DocumentosEmpresa">
                    <Navbar />
                    <DocumentosEmpresa />
                </Route>
                
            </Switch>
        </Router>
    );
}

export default Rutas;