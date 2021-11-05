import React from "react";
import axios from "axios";
import { Formik } from "formik";
import {Button, Col, FormFeedback, FormGroup, Input, Row} from "reactstrap";
import * as Yup from 'yup';
import qs from "qs";
import {useGeneratedId} from "../../../Pages/hooks/useGeneratedId";

export const LeggTilStrekning = ({ apiData, refetch }) => {
    const { generatedId, refetchId } = useGeneratedId()
    
    return (
        <Formik
            initialValues={{
                fraSted: '',
                tilSted: ''
            }}
            onSubmit={async (values, formikHelpers) => {
                values.strekningNummer = generatedId;
                axios.post('/Strekning/lagre', qs.stringify(values))
                    .then(() => refetch())
                    .catch(e => {
                        console.log(e)
                        formikHelpers.setStatus('Det har skjedd en feil! Se console for mer.');
                    });
                formikHelpers.resetForm();
                refetchId();
            }}
            validationSchema={Yup.object().shape({
                fraSted: Yup.string().required('Fyll ut avgang'),
                tilSted: Yup.string().required('Fyll ut ankomst')
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
                } = props;

                return (
                    <form onSubmit={handleSubmit}>
                        <Row>
                            <Col md={6}>
                                <FormGroup>
                                    <label>Avgangslokasjon</label>
                                    <Input
                                        id={'fraSted'}
                                        type={'select'}
                                        value={values.fraSted}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.fraSted && touched.fraSted}
                                    >
                                        <option>Velg avgang</option>
                                        {apiData?.lokasjoner?.map(lokasjoner => {
                                            if (lokasjoner.stedsNummer === values.tilSted) {
                                                return null;
                                            }
                                            return (
                                                <option
                                                    key={lokasjoner.stedsNummer}
                                                    value={lokasjoner.stedsNummer}
                                                >
                                                    {lokasjoner.stedsnavn}
                                                </option>
                                            )})}
                                    </Input>
                                    <FormFeedback>{errors.fraSted}</FormFeedback>
                                </FormGroup>
                            </Col>
                            
                            <Col md={6}>
                                <FormGroup>
                                    <label>Ankomstlokasjon</label>
                                    <Input
                                        id={'tilSted'}
                                        type={'select'}
                                        value={values.tilSted}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.tilSted && touched.tilSted}
                                    >
                                        <option>Velg avgang</option>
                                        {apiData?.lokasjoner?.map(lokasjoner => {
                                            if (lokasjoner.stedsNummer === values.fraSted) {
                                                return null;
                                            }
                                            return (
                                                <option
                                                    key={lokasjoner.stedsNummer}
                                                    value={lokasjoner.stedsNummer}
                                                >
                                                    {lokasjoner.stedsnavn}
                                                </option>
                                            )})}
                                    </Input>
                                    <FormFeedback>{errors.tilSted}</FormFeedback>
                                </FormGroup>
                            </Col>
                        </Row>
                        
                        <Button
                            className={'btn btn-success mt-3'}
                            disabled={isSubmitting}
                        >
                            Legg til strekning
                        </Button>
                    </form>
                );
            }}
        </Formik>
    )
}