import React, { createContext, useContext, useReducer } from 'react';

// almacenamiento y lectura de datos 
export const StateContext = createContext();

// crear proceso de suscripcion para los componentes
export const StateProvider = ({reducer, initialState, children}) => (
    <StateContext.Provider value = {useReducer(reducer, initialState)}>
        {children}
    </StateContext.Provider>
);

export const useStateValue = () => useContext(StateContext);