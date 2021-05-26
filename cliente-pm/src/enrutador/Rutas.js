import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Registro from '../components/login/Registro';
import Home from '../components/login/Home';
import PerfilGeneral from '../components/perfiles/PerfilGeneral';
import Navbar from '../components/Navbar';
import { Pases } from '../components/pases/Pases';
import { Visita } from '../components/pases/Visita';
import RecuperarClave from '../components/login/RecuperarClave';
import CambiarClave from '../components/login/CambiarClave';
import AgregarPersona from '../components/pases/AgregarPersona';
import { Contratista } from '../components/pases/Contratista';
import { DocumentosEmpresa } from '../components/pases/DocumentosEmpresa';
import { AgregarPersonaContratista } from '../components/pases/AgregarPersonaContratista';
import { Tripulante } from '../components/pases/Tripulante';
import { UsoDeMuelle } from '../components/pases/UsoDeMuelle';

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

                <Route path="/SolicitudUsoDeMuelle">
                    <Navbar />
                    <UsoDeMuelle />
                </Route>

                <Route path="/SolicitudTripulante">
                    <Navbar />
                    <Tripulante />
                </Route>

            </Switch>
        </Router>
    );
}

export default Rutas;