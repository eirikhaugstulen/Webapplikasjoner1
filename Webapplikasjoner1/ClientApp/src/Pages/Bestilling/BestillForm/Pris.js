import React from "react";
import {Button, Col, FormFeedback, FormGroup, Input, Label, Row} from "reactstrap";
import {Form, Formik} from "formik";
import * as Yup from 'yup';

export const Pris = ({ tilAvgangPris = 0, fraAvgangPris = 0, setTotalPris, setTotaltAntallBilletter, setStep }) => {
    
    return (
        <Formik 
            initialValues={{
                Ordinær: 0,
                Student: 0,
                Barn: 0,
            }}
            onSubmit={values => {
                let totalPris = ((tilAvgangPris + fraAvgangPris) * values.Ordinær)
                    + (((tilAvgangPris + fraAvgangPris) * values.Student) * 0.75) 
                    + (((tilAvgangPris + fraAvgangPris) * values.Barn) / 2);
                let totalAntallBilletter = values.Ordinær + values.Student + values.Barn;
                
                setTotalPris(totalPris);
                setTotaltAntallBilletter(totalAntallBilletter);
                setStep(5);
            }}
            validationSchema={Yup.object().shape({
                Ordinær: Yup.number().min(1, 'Må være minst en ordinær'),
                Student: Yup.number().min(0, 'Må være et positivt tall'),
                Barn: Yup.number().min(0, 'Må være et positivt tall'),
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
                
                const voksen = ((tilAvgangPris + fraAvgangPris) * values.Ordinær);
                const student = (((tilAvgangPris + fraAvgangPris) * values.Student) * 0.75)
                const barn = (((tilAvgangPris + fraAvgangPris) * values.Barn) / 2);
                
                return (
                    <Form onSubmit={handleSubmit}>
                        <Row>
                            <Col md={6}>
                                <h4 className={'mt-2 mb-3'}>Billettyper</h4>
                            </Col>
                        </Row>
                        <Row>
                            <Col md={6}>
                                <FormGroup>
                                    <Label>Ordinær</Label>
                                    <Input
                                        id={'Ordinær'}
                                        type={'number'}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.Ordinær && touched.Ordinær}
                                    />
                                    <FormFeedback>{errors.Ordinær}</FormFeedback>
                                </FormGroup>
    
                                <FormGroup>
                                    <Label>Student / Honnør</Label>
                                    <Input
                                        id={'Student'}
                                        type={'number'}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.Student && touched.Student}
                                    />
                                    <FormFeedback>{errors.Student}</FormFeedback>
                                </FormGroup>
    
                                <FormGroup>
                                    <Label>Barn</Label>
                                    <Input
                                        id={'Barn'}
                                        type={'number'}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        invalid={errors.Student && touched.Student}
                                    />
                                    <FormFeedback>{errors.Student}</FormFeedback>
                                </FormGroup>
                            </Col>
                            
                            <Col md={6} className={'mt-4'}>
                                <p>Ordinær: {voksen > 0 ? voksen : 0},-</p>
                                <p>Student / Honnør: {student > 0 ? student : 0},-</p>
                                <p>Barn: {barn > 0 ? barn : 0},-</p>
                                <p>Totalpris: {(voksen + student + barn) > 0 ? (voksen + student + barn) : 0 },-</p>
                            </Col>
                        </Row>
                        <Button
                            outline
                            color={'primary'}
                            type={'submit'}
                        >
                            Videre
                        </Button>
                    </Form>
                )
            }}
        </Formik>
    )
}