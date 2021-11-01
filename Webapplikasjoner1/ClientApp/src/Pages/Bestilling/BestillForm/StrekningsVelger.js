import React, {useEffect, useState} from "react";
import {Col, FormFeedback, FormGroup, Input, Label} from "reactstrap";
import axios from "axios";

const fetchAvgangssteder = async (fraStedId) => {
    await new Promise(r => setTimeout(r, 1000))
    return axios.get(`/Billett/HentAlle?${fraStedId}`);
}

export const StrekningsVelger = ({reiselokasjoner, handleChange, values, handleBlur, errors, touched}) => {
    const [outputAnkomststed, setOutputankomststed] = useState();
    
    useEffect(() => {
        fetchAvgangssteder()
            .then(res => setOutputankomststed(reiselokasjoner))
            .catch(e => console.log(e))
    }, [values.fraSted])
    
    return (
        <>
            <Col md={6} sm={12}>
                <FormGroup className={'d-block'}>
                    <Label for={'fraSted'}>Hvor vil du reise fra?</Label>
                    <Input 
                        type={'select'}
                        id={'fraSted'}
                        value={values.fraSted}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        invalid={errors.fraSted && touched.fraSted}
                    >
                        <option
                            disabled
                            value={''}
                        >
                            Velg sted
                        </option>
    
                        {reiselokasjoner?.map(avgang => (
                            <option
                                key={avgang.id}
                                value={avgang.displayName}
                            >
                                {avgang.displayName}
                            </option>
                        ))}
                    </Input>
                    <FormFeedback>{errors.fraSted}</FormFeedback>
                </FormGroup>
            </Col>

            <Col md={6} sm={12}>
                <FormGroup className={'d-block'}>
                    <Label for={'tilSted'}>og hvor vil du reise til?</Label>
                    <Input 
                        type={'select'} 
                        id={'tilSted'} 
                        value={values.tilSted}
                        disabled={values.fraSted === 'default'}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        invalid={errors.tilSted && touched.tilSted}
                    >
                        <option
                            disabled
                            value={''}
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
                    <FormFeedback>{errors.tilSted}</FormFeedback>
                </FormGroup>
            </Col>
        </>
    )
};