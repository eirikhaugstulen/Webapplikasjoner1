import React, {useEffect, useState} from "react";
import {Col, FormGroup, Input, Label} from "reactstrap";

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

export const StrekningsVelger = ({ avgangssted, setAvgangssted, loading, setLoading }) => {
    const [fraReiser, setFraReiser] = useState();
    const [tilReiser, setTilReiser] = useState();

    useEffect(() => {
        setTimeout(() => {
            setFraReiser(inputFraReiser);
            setLoading(false);
        }, 500);
    }, [])

    if (loading) {
        return (
            <div>
                ...Laster inn
            </div>
        )
    }
    
    return (
        <>
            <Col md={6} sm={12}>
                <FormGroup className={'d-block'}>
                    <Label for={'fraSted'}>Hvor vil du reise fra?</Label>
                    <Input 
                        type={'select'} 
                        id={'fraSted'} 
                        value={avgangssted} 
                        onChange={e => setAvgangssted(e)}
                    >
                        <option
                            disabled
                            value={'default'}
                        >
                            Velg sted
                        </option>
    
                        {fraReiser?.map((avgang, index) => (
                            <option
                                key={index}
                                value={avgang.id}
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
                    <Input type={'select'} id={'tilSted'}>
                        <option>Velg sted</option>
                        <option>Bergen</option>
                        <option>Langesund</option>
                        <option>Kristiansand</option>
                    </Input>
                </FormGroup>
            </Col>
        </>
    )
};