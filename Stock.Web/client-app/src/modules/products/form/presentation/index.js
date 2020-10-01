import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField.js";
import SelectField from "../../../../components/inputs/SelectField.js";
//import ProductTypeDropDownPage from "../../../productTypes/dropdown/page/index"
import schema from "../validation";

const ProductForm = props => {
  const { handleSubmit, handleCancel, } = props;

  return (
    <Form onSubmit={handleSubmit}>
      <Field label="Nombre" name="name" component={InputField} type="text" />
      <Field label="Costo" name="costPrice" component={InputField} type="number" 
              parse={v => {
                if (isNaN(Number.parseFloat(v)) === true)
                  return ''
                else 
                  return Number.parseFloat(v)
              }} />
      <Field label="Precio de Venta" name="salePrice" component={InputField} type="number" 
              parse={v => {
                if (isNaN(Number.parseFloat(v)) === true)
                  return ''
                else 
                  return Number.parseFloat(v)
              }} />
      <Field label="Stock" name="stock" component={InputField} type="number" 
              parse={v => {
                if (isNaN(Number.parseFloat(v)) === true)
                  return ''
                else 
                  return Number.parseFloat(v)
              }} />
      <Field label="Categoría" name="productTypeId" component={SelectField} type="select"
              options={props.productTypeOptions}/>
      <Field label="Proveedor" name="providerId" component={SelectField} type="select"
              options={props.providerOptions}/>

      {/*<ProductTypeDropDownPage  />
      */}
      {/*
      <ProductTypeDropDown
        label="Categoría"
        name="productTypeDesc"
        component={SelectField}
        type="select"
        options={productTypesOptions}
      />
      <Field
        label="Proveedores"
        name="providers"
        component={SelectField}
        type="select"
        multiple="multiple"
      />
      */}
      <Button className="product-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="product-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};

ProductForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired
};

export default reduxForm({
  form: "product",
  validate: Validator(schema),
  enableReinitialize: true
})(ProductForm);
/*
ProductForm = reduxForm({
  form: "product",
  validate: Validator(schema),
  enableReinitialize: true
})(ProductForm);

ProductForm = connect(product => {
  return product;
})(ProductForm)

export default ProductForm;
*/
