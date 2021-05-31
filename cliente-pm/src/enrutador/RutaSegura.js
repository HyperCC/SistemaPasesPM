import React from 'react';
import { useStateValue } from '../contexto/store';
import { Redirect, Route } from 'react-router-dom';

function RutaSegura({ component: Component, ...rest }) {
    const [{ sesionUsuario }] = useStateValue();

    return (
        <Route {...rest}
            render={(props) =>
                sesionUsuario ?
                    (sesionUsuario.autenticado === true ? (
                        <Component {...props} {...rest} />
                    )
                        : <Redirect to="/" />
                    ) : <Redirect to="/" />
            }
        />
    );
};

export default RutaSegura;