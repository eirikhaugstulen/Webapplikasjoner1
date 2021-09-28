import React, {useCallback, useMemo, useState} from "react";
export const useBestillingsForm = () => {
    const [avgangssted, setAvgangssted] = useState('default');
    const [ankomststed, setAnkomssted] = useState('default')
    const [fraDato, setFraDato] = useState();
    const [tilDato, setTilDato] = useState();
    const [retur, setRetur] = useState(false);
    const [isTouched, setIsTouched] = useState({});
    const valid = true;
    
    const updateIsTouched = useCallback(inputName => {
        if (!(isTouched?.[inputName])) {
            const newState = {...isTouched};
            newState[inputName] = true;
            setIsTouched(newState);
        }
    }, [isTouched])
    
    const avgangsstedValid = useMemo(() => {
        return !!(isTouched?.avgangssted && avgangssted && avgangssted !== 'default');
    }, [avgangssted, isTouched])
    
    const ankomststedValid = useMemo(() => {
        return !!(isTouched?.ankomststed && ankomststed && ankomststed !== 'default');
    }, [ankomststed, isTouched])
    
    const fraDatoValid = useMemo(() => {
        return !!(fraDato)
    }, [fraDato])
    
    const tilDatoValid = useMemo(() => {
        return !!(retur && tilDato)
    }, [tilDato, retur])
    
    const handleSubmit = async () => {
        let error = false;
        if (avgangsstedValid && ankomststedValid && fraDatoValid) {
            if (retur && !tilDatoValid) {
                error = true;
            }
        } else {
            error = true;
        }
        
        if (!error) {
            //axios.post
            console.log('Godkjent')
        } else {
            console.log('IG')
        }
    };
    
    return [
        {
            avgangsstedState: { avgangssted, setAvgangssted, valid: avgangsstedValid },
            ankomststedState: { ankomststed, setAnkomssted, valid: ankomststedValid },
            fraDatoState: { fraDato, setFraDato, valid: fraDatoValid },
            tilDatoState: { tilDato, setTilDato, valid: tilDatoValid },
            returState: { retur, setRetur },
        },
        valid,
        updateIsTouched,
        handleSubmit,
    ]
}