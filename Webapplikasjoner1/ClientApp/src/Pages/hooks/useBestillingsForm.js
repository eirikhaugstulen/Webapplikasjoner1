import React, {useCallback, useMemo, useState} from "react";
import axios from "axios";
import history from "../../history";
import qs from 'qs';

export const useBestillingsForm = () => {
    const [avgangssted, setAvgangssted] = useState('default');
    const [ankomststed, setAnkomssted] = useState('default')
    const [fraDato, setFraDato] = useState();
    const [tilDato, setTilDato] = useState();
    const [retur, setRetur] = useState(false);
    const [isTouched, setIsTouched] = useState({});
    const [fornavn, setFornavn] = useState('');
    const [etternavn, setEtternavn] = useState('');
    
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
    
    const fornavnValid = useMemo(() => {
        return !!(isTouched?.fornavn && fornavn && fornavn !== '')
    }, [isTouched, fornavn])
    
    const etternavnValid = useMemo(() => {
        return !!(isTouched?.etternavn && etternavn && etternavn !== '')
    }, [isTouched, etternavn])
    
    const valid = useMemo(() => {
        if (avgangsstedValid && ankomststedValid && fraDatoValid && fornavnValid && etternavnValid) {
            if (retur && !tilDatoValid) {
                return false;
            }
        } else {
            return false;
        }
        return true;
    }, [avgangsstedValid, ankomststedValid, fraDatoValid, tilDatoValid, retur, fornavnValid, etternavnValid]);
    
    const handleSubmit = async () => {
        if (retur) {
            
        } else {
            const innBillett = {
            }
            axios.post('/Billett/Lagre', qs.stringify({
                TilSted: ankomststed,
                FraSted: avgangssted,
                Fornavn: fornavn,
                Etternavn: etternavn,
                Dato: fraDato,
                Antall: 3,
            }), {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            })
                .then(() => history.push('/reiser'))
                .catch(e => console.log(e))
        }
    }
    
    return [
        {
            avgangsstedState: { avgangssted, setAvgangssted, valid: avgangsstedValid },
            ankomststedState: { ankomststed, setAnkomssted, valid: ankomststedValid },
            fornavnState: { fornavn, setFornavn, valid: fornavnValid},
            etternavnState: { etternavn, setEtternavn, valid: etternavnValid },
            fraDatoState: { fraDato, setFraDato, valid: fraDatoValid },
            tilDatoState: { tilDato, setTilDato, valid: tilDatoValid },
            returState: { retur, setRetur },
        },
        valid,
        updateIsTouched,
        handleSubmit,
    ]
}