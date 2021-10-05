import React, {useEffect, useState} from "react";
import {Col, FormGroup, Input, Label} from "reactstrap";

export const StrekningsVelger = ({ reiselokasjoner, avgangsstedState, ankomststedState, updateIsTouched, prisState, returState }) => {
    const [fraReiser, setFraReiser] = useState();
    const [outputAnkomststed, setOutputankomststed] = useState();
    
    const changeAvgangsstedHandler = (e) => {
        updateIsTouched('avgangssted');
        avgangsstedState.setAvgangssted(e.target.value);
        setOutputankomststed(reiselokasjoner
            .filter(destinasjon => destinasjon.displayName.toString() !== e.target.value)
        );
    }
    
    const changeAnkomsstedHandler = e => {
        updateIsTouched('ankomststed');
        ankomststedState.setAnkomssted(e.target.value);
    }

    useEffect(() => {
        setFraReiser(reiselokasjoner);
    }, [reiselokasjoner])
    
    useEffect(() => {
        const { avgangssted, valid: avgangsstedValid } = avgangsstedState;
        const { ankomststed, valid: ankomststedValid }  = ankomststedState;
        if ((avgangssted && avgangsstedValid) && (ankomststed && ankomststedValid)) {
            if (avgangssted !== ankomststed) {
                const indexOfAvgang = reiselokasjoner.findIndex(lokasjon => lokasjon.displayName === avgangssted);
                const indexOfAnkomst = reiselokasjoner.findIndex(lokasjon => lokasjon.displayName === ankomststed);
                const difference = Math.abs(indexOfAvgang - indexOfAnkomst);
                let pris = (difference*50)-1;
                if (returState.retur) {
                    pris = pris*2
                }
                prisState.setPris(pris);
            }
        }
    }, [avgangsstedState, ankomststedState, prisState, returState, reiselokasjoner])
    
    return (
        <>
            <Col md={6} sm={12}>
                <FormGroup className={'d-block'}>
                    <Label for={'fraSted'}>Hvor vil du reise fra?</Label>
                    <Input 
                        type={'select'}
                        id={'fraSted'}
                        value={avgangsstedState.avgangssted}
                        onChange={e => changeAvgangsstedHandler(e)}
                    >
                        <option
                            disabled
                            value={'default'}
                        >
                            Velg sted
                        </option>
    
                        {fraReiser?.map(avgang => (
                            <option
                                key={avgang.id}
                                value={avgang.displayName}
                            >
                                {avgang.displayName}
                            </option>
                        ))}
                    </Input>
                </FormGroup>
            </Col>

            <Col md={6} sm={12}>
                <FormGroup className={'d-block'}>
                    <Label for={'tilSted'}>og hvor vil du reise til?</Label>
                    <Input 
                        type={'select'} 
                        id={'tilSted'} 
                        value={ankomststedState.ankomststed}
                        disabled={avgangsstedState.avgangssted === 'default'}
                        onChange={e => changeAnkomsstedHandler(e)}
                    >
                        <option
                            disabled
                            value={'default'}
                        >
                            Velg sted
                        </option>

                        {outputAnkomststed?.map(ankomst => (
                            <option
                                key={ankomst.id}
                                value={ankomst.displayName}
                            >
                                {ankomst.displayName}
                            </option>
                        ))}
                    </Input>
                </FormGroup>
            </Col>
        </>
    )
};