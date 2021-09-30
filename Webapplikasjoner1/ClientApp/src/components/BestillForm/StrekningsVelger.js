import React, {useEffect, useState} from "react";
import {Col, FormGroup, Input, Label} from "reactstrap";

// JSON vil byttes ut med API-kall i del 2
const inputFraReiser = [
    {
        id: 1,
        displayName: 'Bergen',
    },
    {
        id: 2,
        displayName: 'Langesund',
    },
    {
        id: 3,
        displayName: 'Kristiansand',
    }
]

export const StrekningsVelger = ({ avgangsstedState, ankomststedState, updateIsTouched }) => {
    const [fraReiser, setFraReiser] = useState();
    const [outputAnkomststed, setOutputankomststed] = useState();
    
    const changeAvgangsstedHandler = (e) => {
        updateIsTouched('avgangssted');
        avgangsstedState.setAvgangssted(e.target.value);
        setOutputankomststed(inputFraReiser
            .filter(destinasjon => destinasjon.displayName.toString() !== e.target.value)
        );
    }
    
    const changeAnkomsstedHandler = e => {
        updateIsTouched('ankomststed');
        ankomststedState.setAnkomssted(e.target.value);
    }

    useEffect(() => {
        setFraReiser(inputFraReiser);
    }, [])
    
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