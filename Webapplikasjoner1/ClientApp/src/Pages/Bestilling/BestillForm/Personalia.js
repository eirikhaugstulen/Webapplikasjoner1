import React, {useContext, useState} from "react";
import {Form, Formik} from "formik";
import * as Yup from 'yup';
import {KundeContext} from "../../../Context/KundeContext";
import {useGeneratedId} from "../../hooks/useGeneratedId";
import {Button, Col, FormFeedback, FormGroup, Input, Label, Row} from "reactstrap";
import axios from "axios";
import qs from "qs";
import history from "../../../history";

export const Personalia = (
    { 
        setBillettKundeId, 
        setStep 
    }
) => {
    const [valgtNytt, setValgtNytt] = useState(false);
    const { KundeId, setKundeId } = useContext(KundeContext);
    const { generatedId } = useGeneratedId();
    
    if (!valgtNytt && KundeId) {
        return (
            <Row>
                <Col md={3} />
                <Col md={6} className={'border mt-4 p-4 text-center'}>
                    <h4>Du har allerede en bruker</h4>
                    <p>Vil du bruke samme personalia på disse billettene?</p>
                    <Button
                        outline
                        color={'primary'}
                        size={'sm'}
                        onClick={() => {
                            setBillettKundeId(KundeId);
                            setStep(4);
                        }}
                    >
                        Bruk personalia
                    </Button>
                    <Button
                        color={'secondary'}
                        size={'sm'}
                        className={'ml-2'}
                        onClick={() => setValgtNytt(true)}
                    >
                        Legg inn nytt
                    </Button>
                </Col>
                <Col md={3} />
            </Row>
        )
    }
    
    return (
        <Formik 
            initialValues={{
                Fornavn: '',
                Etternavn: '',
                Adresse: '',
                Telefonnummer: '',
                Epost: '',
            }}
            onSubmit={(values, formikHelpers) => {
                values.KundeId = generatedId;
                axios.post('/Kunde/Lagre', qs.stringify(values))
                    .then(() => {
                        setKundeId(generatedId);
                        setBillettKundeId(generatedId);
                        setStep(4);
                    })
                    .catch(e => {
                        console.log(e)
                        formikHelpers.setStatus('Det har skjedd en feil!')
                    })
            }}
            validationSchema={Yup.object().shape({
                Fornavn: Yup.string().required('Påkrevd'),
                Etternavn: Yup.string().required('Påkrevd'),
                Adresse: Yup.string().required('Påkrevd'),
                Telefonnummer: Yup.string().required('Påkrevd')
                    .matches(/^[0-9]+$/, "Må være kun siffer")
                    .min(8, 'Må være 8 siffer')
                    .max(8, 'Må være 8 siffer'),
                Epost: Yup.string()
                    .email('Skriv inn en gyldig epost')
                    .required('Påkrevd'),
            })}
        >
            {props => {
                const {
                    values,
                    touched,
                    errors,
                    status,
                    isSubmitting,
                    handleChange,
                    handleBlur,
                    handleSubmit,
                    resetForm,
                } = props;
                
                return (
                    <Form onSubmit={handleSubmit}>
                        <Row>
                            <Col md={6}>
                                <h4 className={'mt-3'}>Kundepersonalia</h4>
                                {status}
                            </Col>
                        </Row>
                        
                        <Row>
                            <Col md={6}>
                                <FormGroup>
                                    <Label>Fornavn</Label>
                                    <Input
                                        id={'Fornavn'}
                                        type={'text'}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.Fornavn && touched.Fornavn}
                                    />
                                    <FormFeedback>{errors.Fornavn}</FormFeedback>
                                </FormGroup>
                            </Col>
                            <Col md={6}>
                                <FormGroup>
                                    <Label>Etternavn</Label>
                                    <Input
                                        id={'Etternavn'}
                                        type={'text'}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.Etternavn && touched.Etternavn}
                                    />
                                    <FormFeedback>{errors.Etternavn}</FormFeedback>
                                </FormGroup>
                            </Col>
                        </Row>
                        <Row>
                            <Col md={6}>
                                <FormGroup>
                                    <Label>Adresse</Label>
                                    <Input
                                        id={'Adresse'}
                                        type={'text'}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.Adresse && touched.Adresse}
                                    />
                                    <FormFeedback>{errors.Adresse}</FormFeedback>
                                </FormGroup>
                            </Col>
                            <Col md={6}>
                                <FormGroup>
                                    <Label>Telefonnummer</Label>
                                    <Input
                                        id={'Telefonnummer'}
                                        type={'text'}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.Telefonnummer && touched.Telefonnummer}
                                    />
                                    <FormFeedback>{errors.Telefonnummer}</FormFeedback>
                                </FormGroup>
                            </Col>
                        </Row>
                        <Row>
                            <Col md={6}>
                                <FormGroup>
                                    <Label>Epost</Label>
                                    <Input
                                        id={'Epost'}
                                        type={'text'}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.Epost && touched.Epost}
                                    />

                                    <FormFeedback>{errors.Epost}</FormFeedback>
                                </FormGroup>
                            </Col>
                        </Row>
                        <Button
                            outline
                            type={'submit'}
                            color={'primary'}
                            disabled={isSubmitting}
                        >
                            Videre
                        </Button>
                    </Form>
                )
            }}
        </Formik>
    )
}