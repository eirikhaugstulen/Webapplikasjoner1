import React, {createContext} from "react";

export const KundeContext = createContext({
    KundeId: '',
    setKundeId: (kundeId) => {},
})
