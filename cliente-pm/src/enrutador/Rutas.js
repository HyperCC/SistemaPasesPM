import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Registro from '../components/login/Registro';
import Home from '../components/login/Home';
import PerfilGeneral from '../components/perfiles/PerfilGeneral';
import Navbar from '../components/Navbar';
import { Pases } from '../components/pases/SeleccionPases';
import { Visita } from '../components/pases/Visita';
import RecuperarClave from '../components/login/RecuperarClave';
import CambiarClave from '../components/login/CambiarClave';
import AgregarPersona from '../components/pases/AgregarPersona';
import { Contratista } from '../components/pases/Contratista';
import { DocumentosEmpresa } from '../components/pases/DocumentosEmpresa';
import { AgregarPersonaContratista } from '../components/pases/AgregarPersonaContratista';
import RenderFiles from '../components/RenderFiles';
import { Tripulante } from '../components/pases/Tripulante';
import { UsoDeMuelle } from '../components/pases/UsoDeMuelle';
import { Proveedor } from '../components/pases/Proveedor';
import AreaContacto from '../components/aprobacionPases/AreaContacto';


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

                <Route path="/SolicitudTripulante/AgregarPersona">
                    <Navbar />
                    <AgregarPersona />
                </Route>

                <Route path="/SolicitudUsoDeMuelle/AgregarPersona">
                    <Navbar />
                    <AgregarPersona />
                </Route>

                <Route path="/SolicitudProveedor/AgregarPersona">
                    <Navbar />
                    <AgregarPersona />
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

                <Route path="/Archivo">
                    <Navbar />
                    <RenderFiles />
                </Route>
                <Route path="/SolicitudProveedor">
                    <Navbar />
                    <Proveedor />
                </Route>

                <Route path="/SolicitudUsoDeMuelle">
                    <Navbar />
                    <UsoDeMuelle />
                </Route>

                <Route path="/SolicitudTripulante">
                    <Navbar />
                    <Tripulante />
                </Route>

                <Route path="/areaContacto">
                    <Navbar />
                    <AreaContacto />
                </Route>

            </Switch>
        </Router>
    );
}

export default Rutas;