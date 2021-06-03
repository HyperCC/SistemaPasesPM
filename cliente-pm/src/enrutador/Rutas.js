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
import RutaSegura from './RutaSegura';
import { RevisionPase } from '../components/pases/Revision/RevisionPase';


const Rutas = () => {
    return (
        <Router>
            <Navbar />

            <Switch>

                <RutaSegura exact
                    path="/SolicitudVisita/AgregarPersona"
                    component={AgregarPersona} />

                <RutaSegura exact
                    path="/SolicitudContratista/AgregarPersona"
                    component={AgregarPersona} />

                <RutaSegura exact
                    path="/SolicitudTripulante/AgregarPersona"
                    component={AgregarPersona} />

                <RutaSegura exact
                    path="/SolicitudUsoDeMuelle/AgregarPersona"
                    component={AgregarPersona} />

                <RutaSegura exact
                    path="/SolicitudProveedor/AgregarPersona"
                    component={AgregarPersona} />


                <Route path="/" exact>
                    <Home />
                </Route>

                <RutaSegura exact
                    path="/perfil"
                    component={PerfilGeneral} />

                <Route path="/registro">
                    <Registro />
                </Route>

                <Route path="/RecuperarContraseña">
                    <RecuperarClave />
                </Route>
                <Route path="/CambiarContraseña">
                    <CambiarClave />
                </Route>


                <Route path="/DocumentosEmpresa">
                    <Navbar />
                    <DocumentosEmpresa />
                </Route>

                <Route path="/Archivo">
                    <Navbar />
                    <RenderFiles />
                </Route>

                <RutaSegura exact
                    path="/SolicitudPases"
                    component={Pases} />

                <RutaSegura exact
                    path="/SolicitudVisita"
                    component={Visita} />

                <RutaSegura exact
                    path="/SolicitudContratista"
                    component={Contratista} />

                <RutaSegura exact
                    path="/SolicitudProveedor"
                    component={Proveedor} />

                <RutaSegura exact
                    path="/SolicitudUsoDeMuelle"
                    component={UsoDeMuelle} />

                <RutaSegura exact
                    path="/SolicitudTripulante"
                    component={Tripulante} />

                <Route path="/AreaContacto">
                    <Navbar />
                    <AreaContacto />
                </Route>

                <Route path="/RevisarPase">
                    <RevisionPase />
                </Route>

            </Switch>
        </Router>
    );
}

export default Rutas;