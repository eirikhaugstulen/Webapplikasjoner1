import React from "react";
import {Button, Col, FormFeedback, FormGroup, Input, Label, Row} from "reactstrap";
import { Formik } from "formik";
import * as Yup from 'yup';
import axios from "axios";

export const LoggInnAdmin = () => {
    return (
        <Row className={'justify-content-center text-center mt-5 '}>
            <Col 
                sm={12}
                md={6}
                className={'border p-4 rounded-lg'}
            >
                <div>
                    <h5>Logg inn</h5>
                    <Formik 
                        initialValues={{
                            brukernavn: '',
                            passord: '',
                        }}
                        onSubmit={async values => {
                            axios.post('/Billett/LoggInn', values)
                                .then(res => console.log(res))
                                .catch(e => console.log(e))
                        }}
                        validationSchema={Yup.object().shape({
                            brukernavn: Yup.string().required('Påkrevd'),
                            passord: Yup.string().required('Påkrevd').min(8, 'Passord må være 8 bokstaver og tegn'),
                        })}
                    >
                        {props => {
                            const {
                                values,
                                touched,
                                errors,
                                isSubmitting,
                                handleChange,
                                handleBlur,
                                handleSubmit,
                            } = props;
                            
                            return (
                                <form 
                                    onSubmit={handleSubmit}
                                    className={'text-left'}
                                >
                                    <FormGroup>
                                        <Label>Brukernavn</Label>
                                        <Input
                                            id={'brukernavn'}
                                            type={'text'}
                                            value={values.brukernavn}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            invalid={errors.brukernavn && touched.brukernavn}
                                        />
                                        <FormFeedback>{errors.brukernavn}</FormFeedback>
                                    </FormGroup>

                                    <FormGroup>
                                        <Label>Passord</Label>
                                        <Input
                                            id={'passord'}
                                            type={'password'}
                                            value={values.passord}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            invalid={errors.passord && touched.passord}
                                        />
                                        <FormFeedback>{errors.passord}</FormFeedback>
                                    </FormGroup>
                                    
                                    <Button 
                                        color={'primary'}
                                        disabled={isSubmitting}
                                    >
                                        Logg inn
                                    </Button>
                                </form>
                            )
                        }}
                    </Formik>
                </div>
            </Col>
        </Row>
    )
};